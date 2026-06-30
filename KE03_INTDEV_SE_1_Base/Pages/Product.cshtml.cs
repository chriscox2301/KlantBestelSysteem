using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public Product? product { get; set; }
        public IEnumerable<Product> OtherProducts { get; set; } = [];

        public ProductModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet(int id)
        {
            product = _productRepository.GetProductById(id);
            OtherProducts = _productRepository.GetAllProducts().Where(p => p.Id != id).Take(6);
        }
        public IActionResult OnPostAddToCart(int id, int quantity)
        {
            if(quantity == 0)
            {
                quantity = 1;
            }
            Product? product = _productRepository.GetProductById(id);
            if (product == null)
            {
                return RedirectToPage("/Product", new { id = id }); ;
            }
            CartItem item = new CartItem(product.Id, quantity);
            Cart cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();
            cart.AddItem(item);
            HttpContext.Session.SetObject("Cart", cart);

            return RedirectToPage("/Product", new { id = id });
        }
    }
}
