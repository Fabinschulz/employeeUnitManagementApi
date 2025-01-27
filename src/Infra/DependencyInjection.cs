using EmployeeUnitManagementApi.src.Application.Common.Behaviors;
using EmployeeUnitManagementApi.src.Domain.Enums;
using EmployeeUnitManagementApi.src.Domain.Interfaces;
using EmployeeUnitManagementApi.src.Infra.Persistence;
using EmployeeUnitManagementApi.src.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace EmployeeUnitManagementApi.src.Infra
{
    /// <summary>
    /// Provides extension methods for adding services to the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {

        /// <summary>
        /// Adds the user context to the service collection.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void AddUserContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<UserRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }

        /// <summary>
        /// Adds the database context to the service collection.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetValue<string>("PostgreSQLConnection", builder.Configuration.GetConnectionString("PostgreSQLConnection")!)!;
            Console.WriteLine("Initializing Database for API: " + connectionString.Substring(0, 49));

            try
            {
                builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error connecting to database: " + e.Message);
                throw new Exception("Error on postgresql: " + connectionString.Substring(0, 49));
            }

        }

        /// <summary>
        /// Adds JWT authentication to the service collection.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void AddAuthJwt(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                var key = Encoding.ASCII.GetBytes("MySecretKey");
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        /// <summary>
        /// Adds Swagger documentation to the service collection.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void AddSwaggerDoc(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new()
                {
                    Title = "Employee Unit Management API",
                    Version = "v1",
                    Description = "Uma API para gerenciamento de usuários e unidades.",
                    Contact = new()
                    {
                        Name = "Fabio Lima",
                        Email = "fabio.lima19997@gmail.com"
                    }

                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Adds authorization policies to the service collection.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        public static void AddAuthPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole(IdentityData.AdminPolicy));
                opt.AddPolicy("User", policy => policy.RequireRole(IdentityData.UserPolicy));
            });
        }

        /// <summary>
        /// Contains constants for identity policies.
        /// </summary>
        public static class IdentityData
        {
            /// <summary>
            /// Policy for regular users.
            /// </summary>
            public const string UserPolicy = "User";
            /// <summary>
            /// Policy for administrators.
            /// </summary>
            public const string AdminPolicy = "Admin";
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new RoleEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new StatusEnumConverter());
                });

            services.AddHostedService<MigrationHostedService>();
        }
    }
}
