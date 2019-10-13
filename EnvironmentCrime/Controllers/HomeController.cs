using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");

            if (errand == null)
            {
                return View();

            }
            else
            {
                return View(errand);
            }
        }

        public ViewResult Services()
        {
            return View();
        }

        public ViewResult Contact()
        {
            return View();
        }

        public ViewResult FAQ()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);
            
        }

        public ViewResult Thanks()
        {
            HttpContext.Session.Remove("NewErrand");
            return View();
        }

    }
}
