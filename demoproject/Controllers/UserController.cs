using demoproject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace demoproject.Controllers
{
    public class UserController(ILogger<UserController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            var userToCrate = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await userManager.CreateAsync(userToCrate, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction(nameof(SignIn));
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            var hasUser = await userManager.FindByEmailAsync(model.Email);
            var result = await signInManager.PasswordSignInAsync(hasUser, model.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            if (ModelState.IsValid)
            {
                signInManager.SignOutAsync().Wait();
                TempData["success"] = "Çıkış Yapıldı";
                return RedirectToAction("SignIn");
            }
            return RedirectToAction("SignIn");
        }
    }
}
