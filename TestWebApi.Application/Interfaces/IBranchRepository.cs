using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Domain.Entities;

namespace TestWebApi.Application.Interfaces
{
    public interface IBranchRepository
    {
        Task AddAsync(Branch branches,string databasename);
    }
}
