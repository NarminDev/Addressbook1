using Addressbook1.DAL;
using Addressbook1.Models;
using Addressbook1.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Addressbook1.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ContactController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = await _db.Contacts
                .Include(c => c.Category)
                .ToListAsync();
            return View(contacts);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateVM productVM)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            //if (productVM.ImageFile is null)
            //{
            //    ModelState.AddModelError("ImageFile", "Image is required");
            //    return View();
            //}
            //if (!productVM.ImageFile.ContentType.Contains("image/"))
            //{
            //    ModelState.AddModelError("ImageFile", "File must be an image");
            //    return View();
            //}
            //if (productVM.ImageFile.Length > 2 * 1024 * 1024)
            //{
            //    ModelState.AddModelError("ImageFile", "File size can not exceed 2MB");
            //    return View();
            //}
            if (!ModelState.IsValid) return View();
            Contact contact = new Contact()
            {
                Name = productVM.Name,
                Surname = productVM.Surname,
                Phone = productVM.Phone,
                //UserId = productVM.UserId,
                CategoryId = productVM.CategoryId
            };
            //contact.ImageUrl = productVM.ImageFile.SaveImage(_env, "uploads/contacts");

            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            Contact contact = await _db.Contacts.FindAsync(id);
            contact.IsDeleted = true;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            Contact contact = await _db.Contacts.FindAsync(id);
            contact.IsDeleted = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            Contact contact = await _db.Contacts.FindAsync(id);

            UpdateContactVM contactVM = new UpdateContactVM()
            {
                Name = contact.Name,
                Surname = contact.Surname,
                Phone = contact.Phone,
                CategoryId = contact.CategoryId
            };
            return View(contactVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateContactVM contactVM)
        {
            //if (contactVM.ImageFile is null)
            //{
            //    ModelState.AddModelError("ImageFile", "Image is required");
            //    return View();
            //}
            //if (!contactVM.ImageFile.ContentType.Contains("image/"))
            //{
            //    ModelState.AddModelError("ImageFile", "File must be an image");
            //    return View();
            //}
            //if (contactVM.ImageFile.Length > 2 * 1024 * 1024)
            //{
            //    ModelState.AddModelError("ImageFile", "File size can not exceed 2MB");
            //    return View();
            //}

            if (!ModelState.IsValid) return View();
            ViewBag.Categories = await _db.Categories.ToListAsync();
            Contact oldContact = await _db.Contacts.FindAsync(contactVM.Id);

            oldContact.Name = contactVM.Name;
            oldContact.Surname = contactVM.Surname;
            oldContact.Phone = contactVM.Phone;
            oldContact.CategoryId = contactVM.CategoryId;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }









    }
}
