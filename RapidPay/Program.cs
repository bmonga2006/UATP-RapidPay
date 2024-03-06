using RapidPay.Data;
using RapidPay.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Microsoft.Extensions.Logging;
using RapidPay.Extensions;
using RapidPay.Interfaces;
using Microsoft.AspNetCore.Authentication;
using RapidPay.Authentication;
using Microsoft.OpenApi.Models;
using RapidPay.Business;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwaggerModule();
builder.Services.RegisterDBContextModule(builder.Configuration);
builder.Services.RegisterCardModule();
builder.Services.RegisterPaymentFeeModule(builder.Configuration);
builder.Services.RegisterLoggerModule();
builder.Services.AddAuthentication("XAuthToken")
       .AddScheme<AuthenticationSchemeOptions, XAuthTokenAuthenticationHandler>("XAuthToken", options => { });

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();




app.ConfigureExceptionHandler(logger);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
