using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Domain.Entities
{
    public class RestoUser
    {       
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Database { get; private set; }
        public RestoUser(string name,string database)
        {
            Name = name;
            Database = database;
        }
    }
}
