using PrashTechnologies.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrashTechnologies.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IProductRepository productRepository, IStatsRepository statsRepository)
        {
            Products = productRepository;
            Stats = statsRepository;
        }
        public IProductRepository Products { get; }
        public IStatsRepository Stats { get; }
    }
}
