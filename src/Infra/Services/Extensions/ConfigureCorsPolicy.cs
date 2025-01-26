namespace EmployeeUnitManagementApi.src.Infra.Services.Extensions
{

    // Permitir requisições de qualquer origem
    /// <summary>
    /// Provides extension methods for configuring CORS policies.
    /// </summary>
    public static class CorsPolicyExtension
    {
        /// <summary>
        /// Configures the CORS policy to allow any origin, method, and header.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the CORS policy to.</param>
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
