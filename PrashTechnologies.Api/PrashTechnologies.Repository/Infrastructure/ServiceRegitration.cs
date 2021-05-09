using Microsoft.Extensions.DependencyInjection;
using PrashTechnologies.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrashTechnologies.Repository.Infrastructure
{
    public static class ServiceRegitration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IStatsRepository, StatsRepository>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
