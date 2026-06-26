using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FullOrder
    {
        public Order Order { get; set; }
        public List<CartDisplay> Producten {  get; set; } = new List<CartDisplay>();
    }
}
