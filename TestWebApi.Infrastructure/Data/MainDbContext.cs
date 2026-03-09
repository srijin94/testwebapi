using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Domain.Entities;
namespace TestWebApi.Infrastructure.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) 
        :base(options){ }

        public DbSet<RestoUser> RestoUsers => Set<RestoUser>();
    }
}
