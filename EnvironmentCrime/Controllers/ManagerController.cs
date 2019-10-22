using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class ManagerController : Controller
    {
        private IErrandRepository repository;

        public ManagerController(IErrandRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public ViewResult StartManager()
        {
            return View(repository);
        }

        public ViewResult CrimeManager(int id)
        {
            ViewBag.ID = id;
            return View();
        }
    }
}
