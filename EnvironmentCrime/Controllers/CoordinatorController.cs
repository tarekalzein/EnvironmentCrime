using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Http;
using EnvironmentCrime.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Coordinator")]

    public class CoordinatorController : Controller
    {

        private IErrandRepository repository;

        public CoordinatorController(IErrandRepository repo)
        {
            repository = repo;
        }

        // GET: /<controller>/
        public ViewResult StartCoordinator(InvokeRequest request)
        {
            
            ViewBag.ErrandList = repository.GetErrandList(request);
            ViewBag.ListOfDepartments = repository.Departments;

            return View();
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
            TempData["Id"] = id;
            ViewBag.ListOfDepartments = repository.Departments;
            return View();
        }

        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);
        }

        public ViewResult Thanks()
        {
            Errand errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if (errand == null)
            {
                return View();
            }
            else
            {
                int sequenceValue = repository.GetSequence();
                errand.RefNumber = "2018-45-" + sequenceValue;
                ViewBag.NewErrandRefNumber = errand.RefNumber;
                errand.StatusId = "S_A";
                repository.UpdateSequence();
                repository.SaveErrand(errand);
                HttpContext.Session.Remove("NewErrand");
                return View(errand);
            }
        }
        [HttpPost]
        public IActionResult Save(Errand errand)
        {
             
            errand.ErrandId = int.Parse(TempData["Id"].ToString());
            if (errand.DepartmentId != "Välj alla")
            {
                repository.UpdateDepartment(errand);
            }
            
            return RedirectToAction("CrimeCoordinator", new {id= errand.ErrandId });
            }
        [HttpPost]
        public IActionResult Filter(InvokeRequest invokeRequest)
        {
            InvokeRequest request = new InvokeRequest { };
            if (invokeRequest.StatusId != null && invokeRequest.StatusId!= "Välj alla")
            {
                request.StatusId = invokeRequest.StatusId;
            }
            if (invokeRequest.DepartmentId != null && invokeRequest.DepartmentId!= "Välj alla")
            {
                request.DepartmentId = invokeRequest.DepartmentId;
            }
            if (!string.IsNullOrWhiteSpace(invokeRequest.RefNumber))
            {
                request.RefNumber = invokeRequest.RefNumber;
            }

            return RedirectToAction("StartCoordinator", request);
        }       
    }
}
