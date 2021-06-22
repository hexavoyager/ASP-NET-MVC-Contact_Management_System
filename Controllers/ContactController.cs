using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Demo_2.Models.Data;
using MVC_Demo_2.Models.Forms;
using MVC_Demo_2.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo_2.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;
        private readonly CategoryService _categoryService;

        public ContactController(ContactService contactService, CategoryService categoryService)
        {
            _contactService = contactService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View(_contactService.Get());
        }
        public IActionResult Details(int id)
        {
            return View(_contactService.Get(id));
        }
        public IActionResult Create()
        {
            CreateContactForm form = new CreateContactForm();
            form.Categories = GetCategories();
            return View(form);
        }

        private IList<SelectListItem> GetCategories(int? categoryId = null)
        {
            IEnumerable<Category> categories = _categoryService.Get();
            return new List<SelectListItem>
                (categories.Select(c => new SelectListItem
                (c.Name, c.Id.ToString()) 
                { Selected = categoryId.HasValue && c.Id == categoryId.Value }));
        }

        // POST: ContactController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        // GET: ContactController/Edit/5
        public ActionResult Edit(int id)
        {
            Contact contact = _contactService.Get(id);

            if (contact is null)
            {
                return RedirectToAction("Index");
            }
            EditContactForm form = new EditContactForm()
            {
                Id = contact.Id,
                LastName = contact.LastName,
                FirstName = contact.FirstName,
                Email = contact.Email,
                CategoryId = contact.CategoryId,
                Categories = GetCategories()
            };

            return View(form);
        }

        // POST: ContactController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditContactForm form)
        {
            if (!ModelState.IsValid)
            {
                form.Categories = GetCategories(form.CategoryId);
                return View(form);
            }

            Contact contact = new Contact()
            {
                Id = form.Id,
                FirstName = form.FirstName,
                LastName = form.LastName,
                Email = form.Email,
                CategoryId = form.CategoryId
            };

            _contactService.Update(contact);

            return RedirectToAction("Index");
        }

        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            Contact contact = _contactService.Get(id);

            if (contact is null)
            {
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // POST: ContactController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _contactService.Delete(id);
            return RedirectToAction("index");
        }
    }
}
