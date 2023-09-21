using DemoWebApi.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebApi.Controllers
{
    public class ProductController : Controller
    {
        public ProductController() { }
        public IActionResult Detail(string id)
        {
            // get product from db

            // fake product
            var product = new ProductViewDetailModel
            {
                Id = Guid.Empty,
                ShopName = "Shop name",
                CategoryName = "Category",
                Amount = 1000,
                Description = "Description",
                Name = "Product Name",
                Price = 1000000,
                PriceSale = 100000,
                ReviewProd = 5
            };

            return View(product);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
