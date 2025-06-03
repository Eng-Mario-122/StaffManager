using Demo.DAL.Models;
using Demo.PL.HelperClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.PL.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByNameAsync(model.UserName);
                if (User == null)
                {
                    User = new ApplicationUser()
                    {

                        UserName=model.UserName,
                        FName=model.FName,
                        LName=model.LName,
                        Email=model.Email,


                    };
                    var result = await _userManager.CreateAsync(User, model.Password);
                    if (result.Succeeded)

                        return RedirectToAction(nameof(SignIn));
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
                ModelState.AddModelError(string.Empty, "User Name Is already exists");

            }
            return View(model);

        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.Users
                              .Where(u => u.Email == signIn.Email)
                              .FirstOrDefaultAsync();

                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, signIn.Password);
                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, false);
                        if (result.Succeeded)
                            HttpContext.Session.SetString("UserName", user.UserName);
                        HttpContext.Session.SetString("Email", user.Email);

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "invalid signIn");
            }
            return View(signIn);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        public IActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendResetPassUrl(ForgetPass Pass)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(Pass.Email);
                if (user is not null)
                {
                    var ResetPasswordUrl = Url.Action("ResetPass", "Account", new { email = Pass.Email });
                    var email = new Email

                    {
                        Subject="reset your password",
                        Recipients= Pass.Email,
                        Body="ResetPasswordUrl"
                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));

                }
                ModelState.AddModelError(string.Empty, "invalid email");
            }

            return View();
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public ActionResult ResetPass(string email)
        {
            return View();

        }
    }

}
