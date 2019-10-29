using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles ="Manager")]
    public class ManagerController : Controller
    {
        private IErrandRepository repository;

        public ManagerController(IErrandRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        [Authorize(Roles = "Manager")]
        public ViewResult StartManager()
        {
            return View(repository);
        }

        [Authorize(Roles = "Manager")]
        public ViewResult CrimeManager(int id)
        {
            ViewBag.ID = id;
            TempData["id"] = id;
            ViewBag.ListOfEmployees = repository.Employees;
            return View();
        }

        public IActionResult Save(Errand errand)
        {
            errand.ErrandId = int.Parse(TempData["id"].ToString());
            if (errand.StatusId.ToString() == "true")
            {
                repository.UpdateAction(errand);
            }
            if (errand.EmployeeId != "Välj" && errand.StatusId.ToString() != "true")
            {
                repository.UpdateEmployee(errand);
            }
            return RedirectToAction("CrimeManager", new { id = errand.ErrandId });    
        }

    }
}
