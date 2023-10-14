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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? SqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
                             options.UseSqlServer(SqlConnection));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocalityRepository, LocalityRepository>();

//builder.Services.AddTransient<TokenService>();

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
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
