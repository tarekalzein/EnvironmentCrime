using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

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
            return View();
        }

        public ViewResult CrimeCoordinator(string id)
        {
            var errandDetail = from td in repository.Errands
                               where td.ErrandID == id
                               select td;
            return View(errandDetail);
        }

        public ViewResult Validate()
        {
            return View();
        }

        public ViewResult Thanks()
        {
            return View();
        }
    }
}
