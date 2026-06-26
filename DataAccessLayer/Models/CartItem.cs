using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public CartItem (int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

    }
}
