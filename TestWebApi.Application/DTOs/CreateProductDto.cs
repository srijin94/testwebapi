using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Application.DTOs
{
    public record CreateProductDto(string Name, decimal Price, int Stock,string Decs,decimal Cost);
    public record ProductResponseDto(int Id, string Name, decimal Price, int Stock,string Desc);
}
