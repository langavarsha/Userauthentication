using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Userauthentication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Userauthentication.common;

namespace Userauthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  DBContextClass _context;
        private readonly string con = null;

        private Common common;
        public HomeController(DBContextClass dbclass)
        {
         
           // _context = dbclass;          
           // con = dbclass.Database.GetDbConnection().ConnectionString;
            
        }

        public IActionResult Index()
        {
          
            //  _context = new DBContextClass();
            return View();
        }
        public IActionResult CreateUser()
        {
            string sess = HttpContext.Session.GetString("UserName");

            if (sess == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult UserList()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Login(User login)
        {
            _context = new DBContextClass();
                     
            string pass = common.Encrypt(login.Password);

            var loginqry = from lgn in _context.Users
                          
                           where lgn.Password == pass && lgn.Email == login.Email

                           select new { lgn.Email};


            if (loginqry.Count() == 0)
            {
                ViewBag.Message = "Invalid user name or password.";
                return View();
            }
            else
            {


                foreach (var l in loginqry)
                {
                    HttpContext.Session.SetString("UserName", l.Email);
                   
                }
                return RedirectToAction("Index");
            }
            

        }
    }
}
