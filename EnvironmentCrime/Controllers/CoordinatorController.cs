using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class CoordinatorController : Controller
    {
        // GET: /<controller>/
        public ViewResult StartCoordinator()
        {
            return View();
        }

        public ViewResult ReportCrime()
        {
            return View();
        }

        public ViewResult CrimeCoordinator()
        {
            return View();
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
