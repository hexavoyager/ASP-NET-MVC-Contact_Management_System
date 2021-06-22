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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Demo_2.Controllers
{
    public class OldContactController : Controller
    {
        private readonly ContactService _contactService;
        private readonly CategoryService _categoryService;
        public OldContactController(ContactService contactService, CategoryService categoryService)
        {
            Console.WriteLine("CREATION DU CONTROLLEUR CONTACT");
            _contactService = contactService;
            _categoryService = categoryService;
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
            
            CreateContactForm form = new CreateContactForm();
            form.Categories = GetCategories();
            return View(form);
        }

        private IList<SelectListItem> GetCategories()
        {
            IEnumerable<Category> categories = _categoryService.Get();
            return new List<SelectListItem>(categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())));
        }

        [HttpPost]
        public IActionResult Create(CreateContactForm form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = GetCategories();
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
