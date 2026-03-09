using Microsoft.AspNetCore.Mvc;
using TestWebApi.Application.DTOs;
using TestWebApi.Application.Interfaces;
using TestWebApi.Domain.Entities;

namespace TestWebApi.API.Controllers
{
    [ApiController]
    [Route("api/branches")]
    public class BranchController : ControllerBase
    {
       private readonly IBranchRepository _repository;
       public BranchController(IBranchRepository branchRepository)
        {
            _repository = branchRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBranchDto dto, string databasename)
        {
            var branch = new Branch(dto.BranchName, dto.OrderNo);
            await _repository.AddAsync(branch, databasename);
            return CreatedAtAction(nameof(Create), new { id = branch.BranchId }, branch);
        }
    }
}
