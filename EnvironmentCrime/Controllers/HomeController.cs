using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using EnvironmentCrime.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class HomeController : Controller
    {

        private IErrandRepository repository;

        public HomeController(IErrandRepository repo)
        {
            repository = repo;
        }

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
            Errand errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if (errand == null)
            {
                return View();
            }
            else
            {
                int sequenceValue = repository.GetSequence();
                errand.RefNumber = "2018-45-" + sequenceValue;
                ViewBag.NewErrandRefNumber = errand.RefNumber;
                errand.StatusId = "S_A";
                repository.UpdateSequence();
                repository.SaveErrand(errand);
                HttpContext.Session.Remove("NewErrand");
                return View(errand);
            }
         
        }

        

    }
}
