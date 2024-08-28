using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DataIO_Spare.Models;
using AppDbContext.Data;
using DataIO.Models;

namespace DataIO_Spare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Dependency Injection ile ILogger ve ApplicationDbContext ekliyoruz
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult UserList()
        {
            List<UserData> userList = _context.UserData.ToList();
            return View(userList); 
        }

        public IActionResult RefreshUserList()
        {
            return RedirectToAction("UserList");
        }

        // kullanıcıdan gelen bilgileri kaydetme
        [HttpPost]
        public IActionResult SaveUserData(UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.UserData.Add(userData);
                _context.SaveChanges();

                
                ViewBag.IsSuccess = true;

                return RedirectToAction("Index"); 
            }

            return View(userData);
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var userData = _context.UserData.Find(id);
            if (userData != null)
            {
                _context.UserData.Remove(userData);
                _context.SaveChanges();
            }

            return RedirectToAction("UserList");
        }

        [HttpPost]
        public IActionResult EditUser(UserData updatedUserData)
        {
            if (ModelState.IsValid)
            {
                var userData = _context.UserData.Find(updatedUserData.Id);

                userData.FirstName = updatedUserData.FirstName;
                userData.LastName = updatedUserData.LastName;
                userData.TC = updatedUserData.TC;
               
                _context.Update(userData);
                _context.SaveChanges();

                return RedirectToAction("UserList"); 
            }

            return View(updatedUserData); 
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var userData = _context.UserData.Find(id);
            if (userData == null)
            {
                return NotFound();
            }

            return View(userData);
        }



    }
}