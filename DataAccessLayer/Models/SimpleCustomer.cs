using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class SimpleCustomer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SimpleCustomer(int id, string name)
        {
            Id = id; 
            Name = name;
        }
    }
}
