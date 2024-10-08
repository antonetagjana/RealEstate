using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
// using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using WebApplication2.Middleware;
using WebApplication2.Registers;
using WebApplication2.Services;
using WebApplication2.Services.User;

//using WebApplication2.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseLamar((context, registry) =>
{
    registry.IncludeRegistry<WebApplication2.Registers.ServiceRegistry>();
});

// Shtimi i HttpClient me emër specifik për përdorim në klasa të tjera
builder.Services.AddHttpClient("RealEstateAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/"); // Vendos adresën bazë të API-së që do të përdorësh
    client.Timeout = TimeSpan.FromSeconds(30); // Opsionale: Përcakton kohën maksimale që klienti do të presë për përgjigje
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.UseInlineDefinitionsForEnums(); 
});
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("Role", "Admin")); // Ky claim 'Role' duhet të ekzistojë në token-in e përdoruesit dhe të ketë vlerën 'Admin'
    
    options.AddPolicy("MustBeAdminOrSeller", policy =>
        policy.RequireClaim("Role", "Admin", "Seller"));
    
    options.AddPolicy("AuthenticatedUserPolicy", policy =>
        policy.RequireAuthenticatedUser());
    
    
        options.AddPolicy("HRManagerOnly", policy =>
            policy.RequireClaim("Role", "HRManager"));
 

});




builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);


var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
