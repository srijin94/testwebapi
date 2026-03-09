using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application.Interfaces;
using TestWebApi.Application.DTOs;
using TestWebApi.Domain.Entities;
using AutoMapper;
using TestWebApi.Infrastructure.Repositories;
namespace TestWebApi.API.Controllers
{

    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto); // DTO → Entity
            await _repository.AddAsync(customer);

            // Return only Id
            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                new { id = customer.Id }
            );
        }

        // GET: Retrieve customer by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();

            return Ok(customer); // Returns full entity
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerListDto>>> GetCustomerlist()
        {
            var customers = await _repository.GetCustomersAsync();
            return Ok(customers);
        }
        /* [HttpGet("namecount/{name}")]
         public async Task<IActionResult> GetNameCount(string name)
         {
             var result = await _repository.GetCustomersByNameAsync(name);
             return Ok(result);
         }*/
        [HttpGet("namecount/{name}")]
        public async Task GetCustomerNameCount(string name)
        {
            Response.ContentType = "application/json";

            await using var writer = new StreamWriter(Response.Body);
            await writer.WriteAsync("[");
            bool first = true;

            await foreach (var item in _repository.GetCustomersByNameStream(name))
            {
                if (!first) await writer.WriteAsync(",");
                first = false;

                var json = System.Text.Json.JsonSerializer.Serialize(item);
                await writer.WriteAsync(json);
                await writer.FlushAsync();
            }

            await writer.WriteAsync("]");
        }
    }
    
}
