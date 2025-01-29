using EmployeeUnitManagementApi.src.Infra;
using EmployeeUnitManagementApi.src.Infra.Services.Extensions;
using Microsoft.AspNetCore.Rewrite;

var builder = WebApplication.CreateBuilder(args);
var builderServices = builder.Services;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builderServices.AddEndpointsApiExplorer();
builderServices.AddSwaggerGen();
builderServices.ConfigureCorsPolicy(); // Add CORS policy
builderServices.AddControllers();
builderServices.ConfigureServices();
builder.AddUserContext();
builder.AddEmployeeContext();
builder.AddUnitContext();
builder.AddDatabase();
builder.AddSwaggerDoc();
builder.AddAuthPolicy();
builder.AddAuthJwt();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var options = new RewriteOptions().AddRedirect("^$", "swagger/index.html");
app.UseRewriter(options);

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Api v1"));
app.MapSwagger();
app.UseErrorHandler();
app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.MapUserEndpoints();
app.MapEmployeeEndpoints();
app.MapUnitEndpoints();

app.Run();