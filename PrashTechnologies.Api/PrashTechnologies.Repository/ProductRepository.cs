using Microsoft.Extensions.Configuration;
using PrashTechnologies.Application.Interfaces;
using PrashTechnologies.Core.Entities;
using Dapper;
using Dapper.Transaction;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace PrashTechnologies.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id AND IsActive = 1";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products WHERE IsActive = 1";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Product>> GetTopAsync(int top)
        {
            #region parameters
            var parameters = new { TopCount = top };
            #endregion

            var sql = "SELECT TOP (@TopCount) P.*, S.Clicks FROM Products P Inner Join Stats S ON P.Code = S.ProductCode WHERE P.IsActive = 1 AND S.Clicks > 0 ORDER BY S.Clicks DESC";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql, parameters);
                return result.ToList();
            }
        }

        public async Task<int> AddAsync(Product entity)
        {
            // initialize the product code
            var code = Guid.NewGuid();

            #region paramaeters
            var parameters = new { Name = entity.Name, Description = entity.Description, CurrentCost = entity.CurrentCost, Code = code };
            #endregion

            var insertSql = "INSERT INTO Products (Name, Description, CurrentCost, Code) VALUES (@Name, @Description, @CurrentCost, @Code)";
            var statSql = "INSERT INTO Stats (ProductCode) VALUES (@Code)";
            var result = 0;

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                { 
                    result = transaction.Execute(insertSql, parameters);
                    var stats = transaction.Execute(statSql, parameters);

                    transaction.Commit();
                }

                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "UPDATE Products SET IsActive=0 WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
