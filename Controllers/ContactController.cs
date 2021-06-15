using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Demo_2.Models.Data;
using MVC_Demo_2.Tools.Connections.Database;
using Microsoft.Data.SqlClient;
using MVC_Demo_2.Models.Services;
using MVC_Demo_2.Models.Forms;

namespace MVC_Demo_2.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;
        public ContactController(ContactService contactService)
        {
            Console.WriteLine("CREATION DU CONTROLLEUR");
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            Console.WriteLine("APPEL A LA METHODE INDEX");

            return View(_contactService.Get());
        }
        public IActionResult Details(int id)
        {
            Console.WriteLine("APPEL A LA METHODE DETAILS");
            return View(_contactService.Get(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateContactForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            Contact newContact = new Contact()
            {
                LastName = form.LastName,
                FirstName = form.FirstName,
                Email = form.Email,
                CategoryId = form.CategoryId
            };


                _contactService.Insert(newContact);

            return RedirectToAction("Index");
        }
    }
}
