using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Domain.Entities;
using TestWebApi.Application.Interfaces;
using TestWebApi.Infrastructure.Data;
using TestWebApi.Infrastructure.Db;
using Microsoft.EntityFrameworkCore.Internal;

namespace TestWebApi.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContextFactory _dbFactory;

        public ProductRepository(IDbContextFactory dbfactory)
        {
            _dbFactory = dbfactory;
        }

        public async Task AddAsync(Product product,string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task<Product?> GetByIdAsync(int id, string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            return await _context.Products.FindAsync(id);
        }

        public async Task SaveChangesAsync(string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllAsync(string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            return await _context.Products.ToListAsync();
        }

        public async Task DeleteAsync(Product product, string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id, string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            var product = await _context.Products.FindAsync(id);
            if (product == null) return;
            await _context.SaveChangesAsync();
        }

    }
}
