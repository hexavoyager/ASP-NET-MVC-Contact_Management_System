using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MVC_Demo_2.Controllers.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.fname = HttpContext.Session.GetString("firstname");
            ViewBag.lname = HttpContext.Session.GetString("lastname");
            return View();
        }
    }
}
