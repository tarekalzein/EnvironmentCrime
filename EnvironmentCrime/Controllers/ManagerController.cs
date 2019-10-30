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
        public ViewResult StartManager()
        {
            string userDep = repository.GetUserDepartment();
            //return View(repository.Errands.Where(x=> x.DepartmentId==userDep));
            ViewBag.ErrandList = repository.GetMgrErrandList(userDep);
            ViewBag.investigatorList = repository.Employees.Where(x => x.DepartmentId == userDep);
            return View(repository);
        }

        public ViewResult CrimeManager(int id)
        {
            string userDep = repository.GetUserDepartment();
            ViewBag.ID = id;
            TempData["id"] = id;
            ViewBag.ListOfEmployees = repository.Employees.Where(x=>x.DepartmentId== userDep); //Return list of employees that work at the manager's department. PS: The list include the manager him/herself! this can be excluded too.
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
