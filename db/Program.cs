using db.Classes;
using db.Contexts;
using db.Interfaces;
using db.Models;
using db.Repositories;
using db.Repositories.db.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TypeId = int;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register OrderDbContext + reposes
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepository<Order, TypeId>, OrderRepository>();
builder.Services.AddScoped<IRepository<Customer, TypeId>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Driver, TypeId>, DriverRepository>();
builder.Services.AddScoped<IRepository<Rate, TypeId>, RateRepository>();
builder.Services.AddScoped<IRepository<db.Models.Route, TypeId>, RouteRepository>();
builder.Services.AddScoped<IRepository<TransportVehicle, TypeId>, TransportVehicleRepository>();
builder.Services.AddScoped<CredentialRepository>();
builder.Services.AddScoped<RoleRepository>();

// Register JWT
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register JWT as acoped
builder.Services.AddScoped<IJwtService, JwtService>();

// Configure JWT authentification
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        // Получаем настройки из конфигурации
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();