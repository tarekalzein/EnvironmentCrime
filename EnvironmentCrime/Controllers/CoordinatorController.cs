using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EnvironmentCrime.Infrastructure;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class CoordinatorController : Controller
    {
        private IErrandRepository repository;

        public CoordinatorController(IErrandRepository repo)
        {
            repository = repo;
        }

        // GET: /<controller>/
        public ViewResult StartCoordinator()
        {
            return View(repository.Errands);
        }

        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if (errand == null)
            {
                return View();
            }else
            {
                return View(errand);
            }
        }

        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.ID = id;
            return View();
        }

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
