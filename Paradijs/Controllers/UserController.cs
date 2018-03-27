using Paradijs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paradijs.Controllers
{
    
    public class UserController : Controller
    {
       

        // GET: User
        public ActionResult Details()
        {

                return View();
 
        }

        [HttpGet]
        public ActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {

            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {

            return View();
        }
    }
}