using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MatrixIncDbContext _context;

        public OrderRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<FullOrder> GetFullOrdersByCustomer(int customerId)
        {
            List<FullOrder> result = new List<FullOrder>();
            var order = _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToList();

            foreach (var product in order)
            {
                FullOrder fill = new FullOrder();
                fill.Order = product; 
                foreach (var item in product.OrderProducts)
                {
                    CartDisplay p = new CartDisplay(item.Product, item.Quantity);
                    fill.Producten.Add(p);
                }
                result.Add(fill);
            }
            return result;
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.Include(o => o.Customer);
        }

        public Order? GetOrderById(int id)
        {
            return _context.Orders.Include(o => o.Customer).FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}
