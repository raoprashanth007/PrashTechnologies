using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrashTechnologies.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetTopAsync(int top);
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(int id);
    }
}
