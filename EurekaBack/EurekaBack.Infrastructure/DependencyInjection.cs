using EurekaBack.Application.Interfaces;
using EurekaBack.Domain.Interfaces;
using EurekaBack.Infrastructure.Data;
using EurekaBack.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWork = EurekaBack.Infrastructure.UnitOfWork;

namespace EurekaBack.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EurekaContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<EurekaContext>()!);

            return services;
        }
    }
}
