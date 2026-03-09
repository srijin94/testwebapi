using Microsoft.AspNetCore.Mvc;
using TestWebApi.Infrastructure.Repositories;
using TestWebApi.Application.Interfaces;
using TestWebApi.Application.DTOs;
using TestWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace TestWebApi.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto, string databasename)
        {
            var product = new Product(dto.Name, dto.Price, dto.Stock,dto.Decs,dto.Cost);
            await _repository.AddAsync(product,databasename);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateProductDto dto, string databasename)
        {
            var product = await _repository.GetByIdAsync(id, databasename);
            if (product == null)
                return NotFound();
            product.Update(dto.Name, dto.Price, dto.Stock);
            await _repository.SaveChangesAsync(databasename);
            return NoContent(); // 204 success
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string databasename)
        {
            var product = await _repository.GetByIdAsync(id, databasename);

            if (product == null)
                return NotFound();

            await _repository.DeleteAsync(product, databasename);

            return NoContent();
        }
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetAll(string databasename)
        {
            var products = await _repository.GetAllAsync(databasename);

            var result = products.Select(p => new ProductResponseDto(
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.Desc // map your entity property correctly
            ));

            return Ok(result);
        }
    }
}
