using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        /// Action method of index page.
        /// If there are saved session with data, it fills the form automatically on start
        /// </summary>
        /// <returns></returns>
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
        // Simple action methods that open the corresponding pages
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

        /// <summary>
        /// Method that receives the data from form and saves it to a session
        /// </summary>
        /// <param name="errand">object of new errand from form</param>
        /// <returns>The view with the errand data</returns>
        [HttpPost]
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);

        }

        /// <summary>
        /// Gets errand data from the session and saves it to the database
        /// Checkout similar method in coordinator controller
        /// </summary>
        /// <returns></returns>
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
