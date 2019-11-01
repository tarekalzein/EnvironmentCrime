using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl=returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);

                if(user !=null)
                {
                    await signInManager.SignOutAsync();

                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {
                        
                        if(await userManager.IsInRoleAsync(user, "Coordinator"))
                            return Redirect(loginModel?.ReturnUrl ?? "/Coordinator/startCoordinator");

                        if (await userManager.IsInRoleAsync(user, "Manager"))
                            return Redirect(loginModel?.ReturnUrl ?? "/Manager/startManager");

                        if (await userManager.IsInRoleAsync(user, "Investigator"))
                            return Redirect(loginModel?.ReturnUrl ?? "/Investigator/startInvestigator");

                    }
                }
            }
            ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
            return View(loginModel);
        }



        public async  Task<RedirectResult> Logout(string returnUrl ="~/Account/Login")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }

        public ViewResult AccessDenied()
        {
            return View();
        }

       
    }
}