using Addressbook1.Models;
using Addressbook1.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Addressbook1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            AppUser user = new AppUser()
            {
                UserName = registerVM.UserName,
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View(registerVM);
                }
            }
            await _userManager.AddToRoleAsync(user, "User");

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);
            AppUser? user = await _userManager.FindByEmailAsync(loginVM.Email);
            await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
            return RedirectToAction("Index", "Contact");
        }




        // GET: /Account/ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    // Burada email vasitəsilə reset linki göndərmə kodu reallaşdırıla bilər.
                    // Nümunə olaraq istifadəçini bilgiləndirmə səhifəsinə yönləndiririk:
                    return RedirectToAction("Login");
                }

                // Təhlükəsizlik üçün e-poçt tapılmasa belə xətanı fərqləndirməmək tövsiyə olunur.
                ModelState.AddModelError("", "If your email is registered, you will receive password reset instructions.");
            }

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> createroles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("User"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    return Content("Created");
        //}







        //private readonly UserManager<AppUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<AppUser> _signInManager;

        //public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _signInManager = signInManager;
        //}

        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterVM registerVM)
        //{
        //    if (!ModelState.IsValid) return View(registerVM);

        //    AppUser user = new AppUser()
        //    {
        //        UserName = registerVM.UserName,
        //        Name = registerVM.Name,
        //        Surname = registerVM.Surname,
        //        Email = registerVM.Email,
        //    };

        //    IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

        //    if (!result.Succeeded)
        //    {
        //        // Bütün xətaları ModelState-ə əlavə edirik (return dövrün XARİCİNDƏ olmalıdır)
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //        return View(registerVM);
        //    }

        //    // "User" rolu bazada yoxdursa avtomatik yaradılır
        //    if (!await _roleManager.RoleExistsAsync("User"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole("User"));
        //    }

        //    await _userManager.AddToRoleAsync(user, "User");

        //    // Qeydiyyatdan sonra avtomatik giriş edib Ana Səhifəyə yönləndirir
        //    await _signInManager.SignInAsync(user, isPersistent: false);
        //    return RedirectToAction("Index", "Home");
        //}

        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginVM loginVM)
        //{
        //    if (!ModelState.IsValid) return View(loginVM);

        //    AppUser? user = await _userManager.FindByEmailAsync(loginVM.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır.");
        //        return View(loginVM);
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
        //    if (!result.Succeeded)
        //    {
        //        ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır.");
        //        return View(loginVM);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        //// GET: /Account/ForgotPassword
        //[HttpGet]
        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user != null)
        //        {
        //                                return RedirectToAction("Login");
        //        }
        //        ModelState.AddModelError("", "If your email is registered, you will receive password reset instructions.");
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}








    }
}
