
    using E_commerce_2.Models.Interface;
    using E_commerce_2.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace E_commerce_2.Controllers
    {
        public class ProductController : Controller
        {
            private readonly IProduct _Product;

            public ProductController(IProduct Product)
            {
                _Product = Product;
            }
            public async Task<IActionResult> Index()
            {
                var ListProduct = await _Product.GetProducts();
                return View(ListProduct);
            }

            public async Task<IActionResult> Details(int id)
            {
                Product product = await _Product.GetProduct(id);

                return View(product);
            }
        }
    }