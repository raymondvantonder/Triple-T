using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TripleT.Application.Common.Authorization;
using TripleT.Application.Common.Behaviours;
using TripleT.Application.Common.Interfaces.Utilities;

namespace TripleT.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggerBehaviour<,>));

            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            return services;
        }
    }
}
