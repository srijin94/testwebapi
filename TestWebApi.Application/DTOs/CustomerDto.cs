using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Application.DTOs
{  
        public record CreateCustomerDto(string Name,string Mob_No,string Flat_No,string Building_No,string Road_No,string Block_No,string Area);
        public record CustomerListDto(int Id, string Name, string Mob_No);
        public record CustomercountDto(string Name, int Name_Count);
}
