using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ConfirmationModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        public Order? Order { get; set; }
        public ConfirmationModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void OnGet(int orderId)
        {
            Order = _orderRepository.GetOrderById(orderId);
        }
    }
}
