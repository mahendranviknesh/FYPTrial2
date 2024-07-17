using Microsoft.AspNetCore.Mvc;
using DisasterRecoverySystem.Models;
using DisasterRecoverySystem.Data;
using System.Linq;

namespace DisasterRecoverySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var loginUser = _context.Users
                .FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (loginUser != null)
            {
                // Implement user session handling here
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(user);
        }

        // Admin Login
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(Admin admin)
        {
            var loginAdmin = _context.Admins
                .FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);

            if (loginAdmin != null)
            {
                // Implement admin session handling here
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(admin);
        }
    }
}
