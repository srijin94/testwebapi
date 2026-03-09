using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application.DTOs;
using TestWebApi.Infrastructure.Repositories;
using TestWebApi.Application.Interfaces;
namespace TestWebApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertOrder([FromBody] OrderInsertDto order, string databasename)
        {
            if (order.Items == null || order.Items.Count == 0)
                return BadRequest("Order must have at least one item.");

            await _repository.InsertOrderAsync(order,databasename);

            return Ok(new { message = "Order created successfully" });
        }
    }
}
