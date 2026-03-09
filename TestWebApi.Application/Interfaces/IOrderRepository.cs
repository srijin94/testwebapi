using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Application.DTOs;

namespace TestWebApi.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task InsertOrderAsync(OrderInsertDto order,string databasename);
    }
}
