using System;
using System.Collections.Generic;
using System.Text;

namespace PrashTechnologies.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }

        IStatsRepository Stats { get; }
    }
}
