using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderhistoryModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public IEnumerable<Customer> Customers { get; set; }
        public List<FullOrder> Orders { get; set; }
        public Customer? CurrentCustomer { get; set; }
        public OrderhistoryModel(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            Orders = new List<FullOrder>();
        }
        public void OnGet()
        {
            Customers = _customerRepository.GetAllCustomers();
            CurrentCustomer = null;
            SimpleCustomer currentCustomer = HttpContext.Session.GetObject<SimpleCustomer>("Customer");
            if (currentCustomer != null)
            {
                CurrentCustomer = _customerRepository.GetCustomerById(currentCustomer.Id);
                Orders = _orderRepository.GetFullOrdersByCustomer(currentCustomer.Id);
            }
        }
    }
}
