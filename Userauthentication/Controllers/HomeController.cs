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
      
        private readonly  DBContextClass _context;
      
        string sess;
        private Common common = null;
        public HomeController(DBContextClass dbclass)
        {
         
            _context = dbclass;          
         
            
        }
        public IActionResult Index()
        {
            sess = HttpContext.Session.GetString("UserName");

            if (sess == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Login()
        {
          
          
            return View();
        }
        public IActionResult CreateUser()
        {
             sess = HttpContext.Session.GetString("UserName");

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
            sess = HttpContext.Session.GetString("UserName");

            if (sess == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }
               
        }
        public IActionResult Privacy()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Login(User login)
        {
            //_context = new DBContextClass();
            common = new Common();
            string pass = common.Encrypt(login.Password);

            var loginqry = from lgn in _context.User
                          
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
