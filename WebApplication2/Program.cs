using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
// using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2.Services; // Sigurohuni që të përdorni namespace të saktë për ServiceRegistry

var builder = WebApplication.CreateBuilder(args);

// Key sekrete për nënshkrimin e JWT
var key = Encoding.ASCII.GetBytes("Ky është çelësi sekret për JWT");

// Shto shërbimin për autentifikimin me JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://yourdomain.com",  // Zëvendëso me domain-in tënd
        ValidAudience = "https://yourdomain.com", // Zëvendëso me domain-in tënd
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Shto controllers dhe shërbime të tjera
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Konfigurimi i pipeline për kërkesat HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  // Swagger do të jetë në root
    });
}

app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();

// Middleware për autentifikimin dhe autorizimin
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
