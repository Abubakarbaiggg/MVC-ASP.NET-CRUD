using AdminDashboard_UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdminDashboard_UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
		public ProductController(ApplicationDbContext context)
        {   
            _context = context;
		}
        public IActionResult AddProduct()
        {
            return View();
        }
        public IActionResult UpdateProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(product);
		}
		[HttpPost]
        public IActionResult AddProductItem(string?name,string?description,decimal price,string color)
        {
            var newProduct = new Products
            {
                Name = name,
                Description = description,
                Price = price,
                Color = color
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
			return RedirectToAction("Dashboard", "Home");
        }
        public IActionResult DeleteProduct(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

	}
}
