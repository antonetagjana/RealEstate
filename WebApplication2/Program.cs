using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
// using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using WebApplication2.Registers;
//using WebApplication2.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseLamar((context, registry) =>
{
    registry.IncludeRegistry<WebApplication2.Registers.ServiceRegistry>();
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);

var app = builder.Build();

//app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  
    });
}
app.MapControllers();
app.Run();
