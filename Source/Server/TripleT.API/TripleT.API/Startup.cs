using Amazon.Runtime;
using AWS.Logger;
using AWS.Logger.SeriLog;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;
using TripleT.API.Filter;
using TripleT.Application;
using TripleT.Application.Common.Authorization;
using TripleT.Application.Common.Authorization.Models;
using TripleT.Infrastructure;
using TripleT.Infrastructure.Persistence;


namespace TripleT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //string logGroup = configuration["CloudWatchLogging:LogGroup"];
            //string logLevel = configuration["CloudWatchLogging:LogLevel"];

            //AWSLoggerConfig awsLoggerConfiguration = new AWSLoggerConfig(logGroup)
            //{
            //    Region = "af-south-1",
            //    Credentials = new BasicAWSCredentials(configuration["AWS:SNS:EmailingQueue:AccessKeyID"], configuration["AWS:SNS:EmailingQueue:AccessSecretKey"])
            //};

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Logger = logger;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddHealthChecks().AddDbContextCheck<TripleTDbContext>();

            services
                .AddControllers()
                .AddFluentValidation(options => options.AutomaticValidationEnabled = false)
                .AddNewtonsoftJson(options =>
                {
                    //options.SerializerSettings.
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() };
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();

                settings.ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() };
                settings.Converters.Add(new StringEnumConverter());

                return settings;
            };

            services.AddScoped<ExceptionFilter>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", x =>
                {
                    x.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true)
                    .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Triple-T API",
                    Description = "Server for Triple-T Client",
                    Contact = new OpenApiContact
                    {
                        Name = "Raymond van Tonder",
                        Email = string.Empty,
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHealthChecks("/health");

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Triple-T API");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseDefaultFiles();
            //app.UseStaticFiles();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
