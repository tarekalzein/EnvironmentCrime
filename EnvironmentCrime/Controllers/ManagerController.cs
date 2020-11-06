using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private IErrandRepository repository;

        public ManagerController(IErrandRepository repo)
        {
            repository = repo;
        }
        // GET: /<controller>/
        public ViewResult StartManager(InvokeRequest request)
        {
            ViewBag.ErrandList = repository.GetErrandList(request);
            ViewBag.EmployeeList = repository.Employees.Where(x => x.DepartmentId == repository.GetUserDepartment());
            return View();
        }

        public ViewResult CrimeManager(int id)
        {
            string userDep = repository.GetUserDepartment();
            ViewBag.ID = id;
            TempData["id"] = id;
            ViewBag.ListOfEmployees = repository.Employees.Where(x => x.DepartmentId == userDep); //Return list of employees that work at the manager's department. PS: The list include the manager him/herself! this can be excluded too.
            return View();
        }

        /// <summary>
        /// Action method to save changes made to an errand. 
        /// </summary>
        /// <param name="errand">object of errand that will be shown and updated</param>
        /// <returns>Redirect to the Errand detail view <C>CrimeManager</C> with the id of the same errand to show changes</returns>
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

        /// <summary>
        /// Method that creates an object of <c>InvokeRequest</c> (request) to be sent to database and return filtered data.
        /// data is fetched from form (dropdown lists and input textbox) if not null.
        /// </summary>
        /// <param name="invokeRequest">Object of Model <c>InvokeRequest</c></param>
        /// <returns>redirection to the same page but with filtered data using a request object.</returns>
        [HttpPost]
        public IActionResult Filter(InvokeRequest invokeRequest)
        {
            InvokeRequest request = new InvokeRequest { };
            if (invokeRequest.StatusId != null && invokeRequest.StatusId != "Välj alla")
            {
                request.StatusId = invokeRequest.StatusId;
            }
            if (invokeRequest.EmployeeId != null && invokeRequest.EmployeeId != "Välj alla")
            {
                request.EmployeeId = invokeRequest.EmployeeId;
            }
            if (!string.IsNullOrWhiteSpace(invokeRequest.RefNumber))
            {
                request.RefNumber = invokeRequest.RefNumber;
            }

            return RedirectToAction("StartManager", request);
        }

    }
}
