using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Application.Interfaces;
using TestWebApi.Domain.Entities;
using TestWebApi.Infrastructure.Data;
namespace TestWebApi.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IDbContextFactory _dbFactory;
       public BranchRepository(IDbContextFactory contextFactory)
        {
            _dbFactory = contextFactory;
        }
        public async Task AddAsync(Branch branch,string databasename)
        {
            await using var _context = _dbFactory.CreateDbContext(databasename);
            await _context.Branches.AddAsync(branch);
            await _context.SaveChangesAsync();
        }
    }
}
