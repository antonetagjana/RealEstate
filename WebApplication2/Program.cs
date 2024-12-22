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
using Swashbuckle.AspNetCore.Filters;
using WebApplication2.Middleware;
using WebApplication2.models;
using WebApplication2.Registers;
using WebApplication2.Services;
using WebApplication2.Services.Property;
using WebApplication2.Services.PropertyPhoto;
using WebApplication2.Services.Reservation;
using WebApplication2.Services.Role;
using WebApplication2.Services.User;

//using WebApplication2.Middleware;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"JWT Key:{builder.Configuration["Jwt : Key"]}");
Console.WriteLine($"JWT Issuer: {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"JWT Audience: {builder.Configuration["Jwt:Audience"]}");

builder.Host.UseLamar((context, registry) =>
{
    registry.IncludeRegistry<WebApplication2.Registers.ServiceRegistry>();
});


builder.Services.AddHttpClient("RealEstateAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/api/"); 
    client.Timeout = TimeSpan.FromSeconds(30); 
});
builder.Services.AddHttpClient<KafkaService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5098"); // URL e Kafka App
});


builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

});
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddScoped<IPropertyPhotoService, PropertyPhotoService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IRoleService, RoleService>();

// builder.Services.AddScoped<IKafkaService, KafkaService>();


builder.Services.AddSwaggerGen(options =>
{
    //new
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    
    
    // Konfiguro skemën e autorizimit
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    // Filtra për autorizim dhe ngarkimin e skedarëve
    options.OperationFilter<SwaggerFileOperationFilter>(); 
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    
    // Definimi i dokumentit të Swagger
   // options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    
    // Trego enumerimet si vlera inline
    options.UseInlineDefinitionsForEnums(); 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
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


// builder.Services.AddIdentity<User, Role>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("Role", "Admin")); 
    
    options.AddPolicy("MustBeAdminOrSeller", policy =>
        policy.RequireClaim("Role", "Admin", "Seller"));
    
    options.AddPolicy("AuthenticatedUserPolicy", policy =>
        policy.RequireAuthenticatedUser());
    
        options.AddPolicy("HRManagerOnly", policy =>
            policy.RequireClaim("Role", "HRManager"));
 

});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();



app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        try
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting up Swagger UI: {ex.Message}");

        }
    }); 
    app.UseCors("AllowSpecificOrigin");

app.UseStaticFiles(); // This enables serving files from wwwroot by default
app.UseRouting();
// app.UseAuthentication();
// app.UseAuthorization();


app.MapControllers();
app.Run();
}
