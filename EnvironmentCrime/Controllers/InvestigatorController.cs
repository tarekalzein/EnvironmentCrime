using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
        // GET: /<controller>/
        public ViewResult StartInvestigator()
        {
            return View();
        }

        public ViewResult CrimeInvestigator()
        {
            return View();
        }
    }
}
