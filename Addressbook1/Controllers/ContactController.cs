using Addressbook1.DAL;
using Addressbook1.Models;
using Addressbook1.ViewModels.Contacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // BUNU ƏLAVƏ EDİN

namespace Addressbook1.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager; // BUNU ƏLAVƏ EDİN       

        public ContactController(AppDbContext db, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _db = db;
            _env = env;
            _userManager = userManager; // BUNU ƏLAVƏ EDİN
        }
        //public async Task<IActionResult> Index()
        //{
        //    List<Contact> contacts = await _db.Contacts
        //        .Include(c => c.Category)
        //        .ToListAsync();
        //    return View(contacts);
        //}
        public async Task<IActionResult> Index()
        {
            // 1. Hal-hazırda sistemə daxil olan istifadəçinin İD-sini tapırıq
            string currentUserId = _userManager.GetUserId(User);

            // 2. Məlumatları çəkirik, filterləyirik və əlifba sırasına görə düzürük
            List<Contact> contacts = await _db.Contacts
                .Where(c => c.UserId == currentUserId) // Yalnız aktiv istifadəçinin kontaktları
                .Include(c => c.Category)
                .OrderBy(c => c.Name) // ƏSAS ƏLAVƏ: Ada görə əlifba sırası ilə düzür (A-Z)
                .ThenBy(c => c.Surname) // Əgər iki eyni adlı adam varsa, onları da soyadına görə sıralayır
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

            if (!ModelState.IsValid) return View();

            // 1. Sistemə daxil olan istifadəçinin ID-sini tapırıq
            string currentUserId = _userManager.GetUserId(User);

            Contact contact = new Contact()
            {
                Name = productVM.Name,
                Surname = productVM.Surname,
                Phone = productVM.Phone,
                CategoryId = productVM.CategoryId,

                // 2. Tapdığımız ID-ni yeni kontakta mənimsədirik!
                UserId = currentUserId
            };

            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(ContactCreateVM productVM)
        //{
        //    ViewBag.Categories = await _db.Categories.ToListAsync();
        //    //if (productVM.ImageFile is null)
        //    //{
        //    //    ModelState.AddModelError("ImageFile", "Image is required");
        //    //    return View();
        //    //}
        //    //if (!productVM.ImageFile.ContentType.Contains("image/"))
        //    //{
        //    //    ModelState.AddModelError("ImageFile", "File must be an image");
        //    //    return View();
        //    //}
        //    //if (productVM.ImageFile.Length > 2 * 1024 * 1024)
        //    //{
        //    //    ModelState.AddModelError("ImageFile", "File size can not exceed 2MB");
        //    //    return View();
        //    //}
        //    if (!ModelState.IsValid) return View();
        //    Contact contact = new Contact()
        //    {
        //        Name = productVM.Name,
        //        Surname = productVM.Surname,
        //        Phone = productVM.Phone,
        //        //UserId = productVM.UserId,
        //        CategoryId = productVM.CategoryId
        //    };
        //    //contact.ImageUrl = productVM.ImageFile.SaveImage(_env, "uploads/contacts");

        //    await _db.Contacts.AddAsync(contact);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    Contact contact = await _db.Contacts.FindAsync(id);
        //    contact.IsDeleted = true;
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        //[HttpPost]
        //public async Task<IActionResult> Restore(int? id)
        //{
        //    Contact contact = await _db.Contacts.FindAsync(id);
        //    contact.IsDeleted = false;
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // 1. Göndərilən ID-yə əsasən bazadan o kontaktı tapırıq
            var contact = await _db.Contacts.FindAsync(id);

            // 2. Əgər kontakt tapılarsa, onu silirik
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                await _db.SaveChangesAsync(); // Dəyişikliyi bazada yadda saxlayırıq
            }

            // 3. Silmə əməliyyatından sonra yenidən siyahıya (Index səhifəsinə) qayıdırıq
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            Contact contact = await _db.Contacts.FindAsync(id);

            UpdateContactVM contactVM = new UpdateContactVM()
            {
                Id = contact.Id,
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
