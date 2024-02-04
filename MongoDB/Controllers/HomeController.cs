using Microsoft.AspNetCore.Mvc;
using MongoDB.Models;
using System.Diagnostics;

namespace MongoDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ProductService productService = new ProductService();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            bool result = productService.AddProduct(product);
            if (result)
            {
                return RedirectToAction("ViewProducts");
            }
            else
            {
                return View(nameof(Create));
            }
        }
        public IActionResult ViewProducts()
        {
            List<Product> products = productService.GetAllProducts();
            return View(products);
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = productService.GetProductById(id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            bool result = productService.UpdateProduct(product);
            if (result)
            {
                return RedirectToAction("ViewProducts");
            }
            else
            {
                return RedirectToAction(nameof(Edit) , product.Id);
            }
        }

        public IActionResult Delete(Guid id)
        {
            var product = productService.DeleteProductById(id);
            if (product)
            {
                return RedirectToAction("ViewProducts");
            }
            else { return RedirectToAction(nameof(Delete)); }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
