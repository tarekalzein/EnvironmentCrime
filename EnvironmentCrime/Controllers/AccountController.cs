using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnvironmentCrime.Controllers
{
    /// <summary>
    /// Controller class that handles the login, logout and Roles.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Constructor with initialization of UserManager and SignInManager
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>returnUrl of login</returns>
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new LoginModel
            {
                ReturnUrl = returnUrl
            });
        }

        /// <summary>
        /// Meothd <c>Login</c> that takes parameter loginModel and validates it with Login Db.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>returns view with the assigned returnUrl according to login.</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.UserName);

                if (user != null)
                {
                    await signInManager.SignOutAsync();

                    if ((await signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                    {

                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
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


        /// <summary>
        /// Method <c>Logout</c>
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>returnUrl after logout</returns>
        public async Task<RedirectResult> Logout(string returnUrl = "~/Account/Login")
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