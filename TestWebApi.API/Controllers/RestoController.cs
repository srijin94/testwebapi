using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application.DTOs;
using TestWebApi.Application.Interfaces;
using TestWebApi.Domain.Entities;
namespace TestWebApi.API.Controllers
{
    [ApiController]
    [Route("api/resto")]
    public class RestoController : ControllerBase
    {
        private readonly IRestoRepository _repository;
        public RestoController(IRestoRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(InsertRestoDto dto)
        {
            var restouser = new RestoUser(dto.Name,dto.Database);
            await _repository.InsertResto(restouser);
            return CreatedAtAction(nameof(Create), new { id = restouser.Id }, restouser);
        }
    }
}
