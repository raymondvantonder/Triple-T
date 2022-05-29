using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripleT.User.Application.Common.Interfaces.Infrastructure;
using TripleT.User.Application.Common.Interfaces.Infrastructure.Persistence;
using TripleT.User.Infrastructure.Emailing;
using TripleT.User.Infrastructure.Persistence;

namespace TripleT.User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dynamoDbClient = new AmazonDynamoDBClient();
            
            services.AddSingleton<IAmazonDynamoDB>(sp => dynamoDbClient);

            var dynamoDbConfig = new DynamoDBContextConfig
            {
                //TableNamePrefix = configuration["ASPNETCORE_ENVIRONMENT"]
            };
            
            services.AddTransient<IDynamoDBContext>(sp => new DynamoDBContext(dynamoDbClient, dynamoDbConfig));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPasswordRepository, PasswordRepository>();
            services.AddTransient<IEmailingService, EmailingService>();

            return services;
        }
    }
}