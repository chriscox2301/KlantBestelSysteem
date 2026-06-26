using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CartDisplay
    {
        public Product product {  get; set; }
        public int quantity { get; set; }

        public CartDisplay(Product product, int quantity)
        {
            this.product = product;
            this.quantity = quantity;
        }
    }
}
