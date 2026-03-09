using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApi.Domain.Entities;
using TestWebApi.Application.DTOs;
namespace TestWebApi.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customers);
        Task<Customer?> GetByIdAsync(int id);
        Task<List<CustomerListDto>> GetCustomersAsync();
        //  Task<List<CustomercountDto>> GetCustomersByNameAsync(string name);
        IAsyncEnumerable<CustomercountDto> GetCustomersByNameStream(string name);
    }
}
