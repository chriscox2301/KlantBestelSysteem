using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using KE03_INTDEV_SE_1_Base.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        public Cart Cart { get; set; }
        public CartDisplay ItemInCart { get; set; }
        public Customer? CurrentCustomer { get; set; }
        public List<CartDisplay> Items { get; set; }
        public decimal Total { get; set; } = 0;
        public Product Product { get; set; }

        public CartModel(IProductRepository productRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            Items = new List<CartDisplay>();
            _orderRepository = orderRepository;
        }

        public void OnGet()
        {
            CurrentCustomer = null;
            SimpleCustomer currentCustomer = HttpContext.Session.GetObject<SimpleCustomer>("Customer");
            if (currentCustomer != null)
            {
                CurrentCustomer = _customerRepository.GetCustomerById(currentCustomer.Id);
            }
            Cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();
            if (Cart.Items.Any())
            {
                foreach (var item in Cart.Items)
                {
                    Product = _productRepository.GetProductById(item.ProductId);
                    ItemInCart = new CartDisplay(Product, item.Quantity);
                    Items.Add(ItemInCart);
                }
            }
            foreach (var item in Items)
            {
                Total += item.product.Price * item.quantity;
            }
        }

        public IActionResult OnPostOrderCart()
        {
            SimpleCustomer currentCustomer = HttpContext.Session.GetObject<SimpleCustomer>("Customer");
            CurrentCustomer = _customerRepository.GetCustomerById(currentCustomer.Id);
            Cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();

            Order order = new Order
            {
                CustomerId = CurrentCustomer.Id,
                OrderDate = DateTime.Now
            };
            foreach (var item in Cart.Items)
            {
                OrderProduct orderProduct = new OrderProduct
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                order.OrderProducts.Add(orderProduct);
            }
            _orderRepository.AddOrder(order);
            HttpContext.Session.SetObject("Cart", new Cart());
            return RedirectToPage("/cart");
        }

        public IActionResult OnPostDeleteCartItem(int id)
        {
            Cart = HttpContext.Session.GetObject<Cart>("Cart");

            CartItem item = Cart.Items.Where(c => c.ProductId == id).First();
            Cart.Items.Remove(item);

            HttpContext.Session.SetObject("Cart", Cart);

            return RedirectToPage("/cart");
        }
    }
}
