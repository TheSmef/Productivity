using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Productivity.API.Data.Context;
using Productivity.API.Data.Repositories;
using Productivity.API.Data.Repositories.Interfaces;
using Productivity.API.Services.Authentication;
using Productivity.API.Services.Authentication.Base;
using Productivity.API.Services.Data;
using Productivity.API.Services.Data.Interfaces;
using Productivity.API.Services.ExportServices;
using Productivity.API.Services.ExportServices.Interfaces;
using Productivity.API.Services.Messaging.Base.Interfaces;
using Productivity.API.Services.Messaging.Base;
using Productivity.API.Services.Middleware;
using Productivity.API.Services.Stats;
using Productivity.API.Services.Stats.Base;
using Productivity.Shared.Models.Utility;
using Productivity.Shared.Utility.AutoMapper;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Productivity.API.Services.Messaging.Interfaces;
using Productivity.API.Services.Messaging;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddExceptionHandler<ErrorHandler>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        .WithHeaders(builder.Configuration.GetSection("AppSettings:AllowedHeaders").Value!.Split(','))
        .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme
    ).AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("AppSettings:SecurityKey").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"))
);

builder.Services.Configure<RabbitMqConfiguration>(a => builder.Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<ICultureRepository, CultureRepository>();
builder.Services.AddScoped<IProductivityRepository, ProductivityRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<ICultureService, CultureService>();
builder.Services.AddScoped<IProductivityService, ProductivityService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IProductivityFileService,
    ProductivityFileService>();

builder.Services.AddScoped<IProductivityStatsService,
    ProductivityStatsService>();

builder.Services.AddScoped<ICultureReportMessagingService,
    CultureReportMessagingService>();
builder.Services.AddScoped<IProductivityReportMessagingService,
    ProductivityReportMessagingService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseExceptionHandler(_ => { });

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
