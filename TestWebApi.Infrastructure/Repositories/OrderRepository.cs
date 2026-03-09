using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;
using TestWebApi.Application.DTOs;
using TestWebApi.Infrastructure.Data;
using TestWebApi.Application.Interfaces;
namespace TestWebApi.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbContextFactory _dbFactory;

        public OrderRepository(IDbContextFactory dbfactory)
        {
            _dbFactory = dbfactory;
        }

        public async Task InsertOrderAsync(OrderInsertDto order,string databasename)
        {
           
                if (order.Items == null || order.Items.Count == 0)
                    throw new ArgumentException("Order must have at least one item");
            await using var _context = _dbFactory.CreateDbContext(databasename);
            var conn = _context.Database.GetDbConnection();
                await conn.OpenAsync();

                // Use a transaction for safety
                await using var transaction = await conn.BeginTransactionAsync();
                await using var cmd = conn.CreateCommand();
                cmd.Transaction = transaction;

                cmd.CommandText = @"CALL public.insert_order_master_details(
                                @customer_id,
                                @branch_id,
                                @items,
                                @order_date
                            )";

                cmd.Parameters.Add(new NpgsqlParameter("customer_id", NpgsqlDbType.Integer)
                {
                    Value = order.CustomerId
                });
                cmd.Parameters.Add(new NpgsqlParameter("branch_id", NpgsqlDbType.Integer)
                {
                    Value = order.BranchId
                });

            cmd.Parameters.Add(new NpgsqlParameter("items", NpgsqlDbType.Json)
                {
                    Value = JsonSerializer.Serialize(order.Items)
                });

                // Convert DateTime to unspecified kind to avoid PostgreSQL errors
                var orderDate = order.OrderDate ?? DateTime.Now;
                cmd.Parameters.Add(new NpgsqlParameter("order_date", NpgsqlDbType.Timestamp)
                {
                    Value = DateTime.SpecifyKind(orderDate, DateTimeKind.Unspecified)
                });

                await cmd.ExecuteNonQueryAsync();
                await transaction.CommitAsync();
            
        }

    }
}
