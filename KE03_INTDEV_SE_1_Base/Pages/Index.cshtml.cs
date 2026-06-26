using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public IList<Product> BestSellingProducts { get; set; }

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            BestSellingProducts = new List<Product>();
        }

        public void OnGet()
        {            
            BestSellingProducts = _productRepository.GetBestSellingProducts(3);
        }
    }
}
