using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public ViewResult Index()
        {
            return View();
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
            if (ModelState.IsValid)
            {
        
                return View(errand);
            }
            else
            {
                return View();
            }
            
        }

        public ViewResult Thanks()
        {
            return View();
        }

    }
}
