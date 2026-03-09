using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Domain.Entities;
using TestWebApi.Application.Interfaces;
using TestWebApi.Infrastructure.Data;
using TestWebApi.Application.DTOs;
using NpgsqlTypes;
using Npgsql;
using System.Threading.Channels;

namespace TestWebApi.Infrastructure.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Customer customer)
        {          
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task<Customer?> GetByIdAsync(int id)
        {
            // Find customer by Id
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Using PostgreSQL Stored Procedure
        public async Task<List<CustomerListDto>> GetCustomersAsync()
        {
            // Assume SP: get_customer_list()
            return await _context.Set<CustomerListDto>()
                .FromSqlRaw("SELECT * FROM get_customer_list()")
                .ToListAsync();
        }
        /* public async Task<List<CustomercountDto>> GetCustomersByNameAsync(string name)
         {
             var customers = new List<CustomercountDto>();

             var conn = _context.Database.GetDbConnection();
             await conn.OpenAsync();

             using var transaction = await conn.BeginTransactionAsync();

             try
             {
                 // 1️⃣ Call the procedure
                 using (var cmd = conn.CreateCommand())
                 {
                     cmd.Transaction = transaction;
                     cmd.CommandText = "CALL customer_name_count(@name_str, @cursor)";

                     // Use NpgsqlParameter explicitly
                     cmd.Parameters.Add(new NpgsqlParameter("name_str", NpgsqlDbType.Text) { Value = name });
                     cmd.Parameters.Add(new NpgsqlParameter("cursor", NpgsqlDbType.Refcursor) { Value = "cur" });

                     await cmd.ExecuteNonQueryAsync();
                 }

                 // 2️⃣ Fetch cursor results
                 using (var cmd = conn.CreateCommand())
                 {
                     cmd.Transaction = transaction;
                     cmd.CommandText = "FETCH ALL FROM cur";

                     using var reader = await cmd.ExecuteReaderAsync();
                     while (await reader.ReadAsync())
                     {
                         customers.Add(new CustomercountDto(
                             reader["name"].ToString()!,
                             Convert.ToInt32(reader["name_count"])
                         ));
                     }
                 }

                 await transaction.CommitAsync();
             }
             catch
             {
                 await transaction.RollbackAsync();
                 throw;
             }

             return customers;
         }*/

        public IAsyncEnumerable<CustomercountDto> GetCustomersByNameStream(string name)
        {
            var channel = Channel.CreateUnbounded<CustomercountDto>();

            _ = Task.Run(async () =>
            {
                var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();
                await using var transaction = await conn.BeginTransactionAsync();

                try
                {
                    // Call procedure
                    await using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = "CALL public.customer_name_count(@name_str, @cursor)";
                        cmd.Parameters.Add(new NpgsqlParameter("name_str", NpgsqlDbType.Text) { Value = name });
                        cmd.Parameters.Add(new NpgsqlParameter("cursor", NpgsqlDbType.Refcursor) { Value = "cur" });
                        await cmd.ExecuteNonQueryAsync();
                    }

                    // Fetch cursor
                    await using (var cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = "FETCH ALL FROM cur";
                        await using var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            await channel.Writer.WriteAsync(new CustomercountDto(
                                reader["name"].ToString()!,
                                Convert.ToInt32(reader["name_count"])
                            ));
                        }
                    }

                    await transaction.CommitAsync();
                    channel.Writer.Complete();
                }
                catch (Exception ex)
                {
                    channel.Writer.Complete(ex);
                    await transaction.RollbackAsync();
                }
            });

            return channel.Reader.ReadAllAsync();
        }
    }
}
