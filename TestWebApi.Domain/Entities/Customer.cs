using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Domain.Entities
{
    public class Customer
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = default!;
        public string Mob_No { get; private set; }
        public string Flat_No { get; private set; }
        public string Building_No { get; private set; }
        public string Road_No { get; private set; }
        public string Block_No { get; private set; }
        public string Area { get; private set; }
        private Customer() { } // EF Core
        public Customer(string name, string mob_no, string flat_no,string building_no,string road_no,string block_no,string area)
        {
            Name = name;
            Mob_No = mob_no;
            Flat_No = flat_no;
            Building_No = building_no;
            Road_No = road_no;
            Block_No = block_no;
            Area = area;            
        }
    }
}
