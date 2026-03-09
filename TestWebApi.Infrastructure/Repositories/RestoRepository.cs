using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application.DTOs;
using TestWebApi.Application.Interfaces;
using TestWebApi.Domain.Entities;
using TestWebApi.Infrastructure.Data;
namespace TestWebApi.Infrastructure.Repositories
{
    public class RestoRepository :IRestoRepository
    {
        private readonly MainDbContext _context;
        public RestoRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task InsertResto(RestoUser resto)
        {
            await _context.RestoUsers.AddAsync(resto);
            await _context.SaveChangesAsync();
        }
    }
}
