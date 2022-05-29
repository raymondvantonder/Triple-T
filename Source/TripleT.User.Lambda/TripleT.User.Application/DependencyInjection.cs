using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TripleT.User.Application.Common.Authorization;
using TripleT.User.Application.Common.Behaviours;
using TripleT.User.Application.Common.Interfaces.Utilities;

namespace TripleT.User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerBehaviour<,>));

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            return services;
        }
    }
}
