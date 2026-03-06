using AdminDashboard_UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminDashboard_UI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var data = _context.Products.ToList();
			return View(data);
        }
        public IActionResult PasswordIncorrect()
        {
            return View();
        }
        public IActionResult UserNotFound()
        {
            return View();
        }
        public IActionResult UpdateProduct(int productId)
        {
            var data = _context.Products.FirstOrDefault(p => p.ProductId == productId);
            return View(data);

        }
        [HttpPost]
        public IActionResult UpdateProduct(string name,string description,decimal price,string color,int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == name);
            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Color = color;
            _context.Products.Update(product);
            _context.SaveChanges();
            return RedirectToAction("Dashboard","Home");
        }
        public IActionResult ExistingUserFound()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegisterUser(string username,string email,string password,string confirmpassword)
        {
            if(password != confirmpassword)
            {
                return View("PasswordIncorrect");
            }
            var existingUser = _context.Users.FirstOrDefault(u => u.Name == username || u.Email == email);
            if(existingUser != null)
            {
                return View("ExistingUserFound");
            }
            _context.Users.Add(new Users
            {
                Name = username,
                Email = email,
                Password = password,
                Role = "User"
            });
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Login(string username,string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == username);
            if(user == null)
            {
                return View("UserNotFound");
            }
            if(user.Password != password)
            {
                return View("PasswordIncorrect");
            }
            return View("Dashboard");
        }
    }
}
