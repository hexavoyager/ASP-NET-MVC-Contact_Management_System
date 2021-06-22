using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_Demo_2.Models.Data;
using MVC_Demo_2.Models.Forms;
using MVC_Demo_2.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo_2.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginContactForm form)
        {
            User user = _userService.Get(form.Email, form.Passwd);

            if (user is not null)
            {
                HttpContext.Session.SetInt32("id", user.Id);
                HttpContext.Session.SetString("email", user.Email);
                HttpContext.Session.SetString("firstname", user.FirstName);
                HttpContext.Session.SetString("lastname", user.LastName);
    
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid information!");
            return View(form);
        }        
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterContactForm form)
        {
            User u = new User()
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                Passwd = form.Passwd
            };

            _userService.Insert(u);

            return RedirectToAction("Index", "Contact");
        }

    }
}
