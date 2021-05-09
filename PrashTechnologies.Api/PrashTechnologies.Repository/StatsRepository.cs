using Microsoft.Extensions.Configuration;
using PrashTechnologies.Application.Interfaces;
using PrashTechnologies.Core.Entities;
using Dapper;
using Dapper.Transaction;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace PrashTechnologies.Repository
{
    public class StatsRepository : IStatsRepository
    {
        private readonly IConfiguration configuration;
        public StatsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<int> AddAsync(Stats entity)
        {
            var sql = "UPDATE Stats SET Clicks = Clicks + 1 WHERE ProductCode = @ProductCode";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Stats>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Stats> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Stats>> GetTopAsync(int top)
        {
            throw new System.NotImplementedException();
        }
    }
}
