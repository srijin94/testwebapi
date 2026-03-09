using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Domain.Entities;

namespace TestWebApi.Application.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, string databasename);
        Task<Product?> GetByIdAsync(int id, string databasename);
        Task SaveChangesAsync(string databasename);
        Task DeleteAsync(Product product, string databasename);
        Task SoftDeleteAsync(int id, string databasename);
        Task<List<Product>> GetAllAsync(string databasename);

    }
}
