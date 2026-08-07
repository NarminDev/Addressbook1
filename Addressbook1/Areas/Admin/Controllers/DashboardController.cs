using Addressbook1.Areas.Admin.ViewModels.Dashboard;
using Addressbook1.DAL;
using Addressbook1.Models;
using Addressbook1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Addressbook1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Ümumi istifadəçi sayı
            int totalUsers = await _userManager.Users.CountAsync();

            // 2. Ümumi kontakt sayı
            int totalContacts = await _db.Contacts.CountAsync();

            // 3. Ən çox kontaktı olan kateqoriya
            var topCategoryData = await _db.Contacts
                .GroupBy(c => c.CategoryId)
                .Select(g => new { CategoryId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .FirstOrDefaultAsync();

            string topCatName = "Məlumat Yoxdur";
            int topCatCount = 0;

            if (topCategoryData != null && topCategoryData.CategoryId != null)
            {
                var category = await _db.Categories.FindAsync(topCategoryData.CategoryId);
                if (category != null)
                {
                    topCatName = category.Name;
                    topCatCount = topCategoryData.Count;
                }
            }

            DashboardVM vm = new DashboardVM
            {
                TotalUsers = totalUsers,
                TotalContacts = totalContacts,
                TopCategoryName = topCatName,
                TopCategoryCount = topCatCount
            };

            return View(vm);
        }
    }
}