using DesafioDotnet_balta.Service;
using DesafioDotnet_balta.Configuration;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Context;
using Repository.Interface;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using DesafioDotnet_balta.DTOs.Mappings;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfraSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Desafio - Balta",
        Version = "v1",
        Description = "Criação de API ",
        TermsOfService = new Uri("https://baltaio.blob.core.windows.net/temp/desafio-dotnet/01-sobre.pdf"),
        Contact = new OpenApiContact
        {
            Name = "Monica Lima",
            Email = "flima.monica@gmail.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
string? SqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                             options.UseSqlServer(SqlConnection));
var MappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = MappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = new HeaderApiVersionReader("X-api-version");
});


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocalityRepository, LocalityRepository>();



Configuration.PrivateKey = builder.Configuration["[PrivateKey]"];

var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        //ValidIssuer = builder.Configuration["jwt:issuer"],
        //ValidAudience = builder.Configuration["jwt:audience"],
    };
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Localidades e Usuario");
    });
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
