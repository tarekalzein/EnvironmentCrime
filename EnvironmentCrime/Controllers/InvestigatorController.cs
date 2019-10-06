using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
        private IErrandRepository repository;

        public InvestigatorController(IErrandRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public ViewResult StartInvestigator()
        {
            return View(repository.Errands);
        }

        public ViewResult CrimeInvestigator()
        {
            return View();
        }
    }
}
