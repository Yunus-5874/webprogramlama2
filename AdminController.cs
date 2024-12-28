using Microsoft.AspNetCore.Mvc;
using BerberWebSitesi.Data;
using BerberWebSitesi.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BerberWebSitesi.Controllers
{
    public class AdminController : Controller
    {
        private readonly BerberContext _context;

        public AdminController(BerberContext context)
        {
            _context = context;
        }

        // Admin giriş sayfasını göster
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);

            if (admin != null)
            {
                HttpContext.Session.SetString("Admin", "LoggedIn");
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }
        }

        // Admin paneli ana sayfası
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") != "LoggedIn")
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // Admin çıkışı
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

