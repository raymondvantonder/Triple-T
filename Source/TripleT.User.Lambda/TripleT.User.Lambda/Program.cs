using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TripleT.User.Application;
using TripleT.User.Infrastructure;
using TripleT.User.Lambda;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.ConfigureExceptionHandler(logger);

app.MapControllers();

app.MapGet("/", ([FromServices] ILogger<Program> logger) =>
{
    logger.LogInformation("Executing welcome");
    return "Welcome to Triple-T user API :)";
});

app.Run();