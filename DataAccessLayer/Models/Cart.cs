using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem (CartItem item)
        {
            CartItem? Item = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (Item == null)
            {
                Items.Add(item);
            }
            else
            {
                Item.Quantity += item.Quantity;
            }
        }

        public void DeleteItem (int id)
        {
            Items.RemoveAll(c => c.ProductId == id);
        }

        public void ChangeQuantity (int id, int quantity)
        {
            Items.RemoveAll(c => c.ProductId == id);
            CartItem item = new CartItem(id, quantity);
            Items.Add(item);
        }
            
    }
}
