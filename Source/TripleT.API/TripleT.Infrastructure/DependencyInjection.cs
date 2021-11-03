using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripleT.Application.Common.Interfaces.Infrastructure;
using TripleT.Application.Interfaces.Infrastructure;
using TripleT.Infrastructure.Emailing;
using TripleT.Infrastructure.Persistence;

namespace TripleT.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["UseInMemoryDatabase"]))
            {
                services.AddDbContext<TripleTDbContext>(options =>
                    options.UseInMemoryDatabase("TripleTDatabase"));
            }
            else
            {
                string mySqlConnectionStr = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContextPool<TripleTDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));


                //services.AddDbContext<TripleTDbContext>(options =>
                //    options.UseSqlServer(
                //        configuration.GetConnectionString("DefaultConnection"),
                //        b => b.MigrationsAssembly(typeof(TripleTDbContext).Assembly.FullName)));
            }

            services.AddScoped<ITripleTDbContext>(provider => provider.GetService<TripleTDbContext>());

            services.AddTransient<IEmailingService, EmailingService>();

            return services;
        }
    }
}
