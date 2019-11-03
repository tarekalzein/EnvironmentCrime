using EnvironmentCrime.Infrastructure;
using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// <summary>
        /// Action method <c>StartCoordinator</c> that returns the view with tables of errands.
        /// List of errands are passed as viewbag
        /// ListofDepartments are passed as viewbag =>shown as dropdown list. kommunen is excluded from the list (from repository)
        /// </summary>
        /// <param name="request">takes a request as parameter which decided which data to be fetched from database.</param>
        /// <returns>View with table of errands filtered as request object.</returns>
        public ViewResult StartCoordinator(InvokeRequest request)
        {

            ViewBag.ErrandList = repository.GetErrandList(request);
            ViewBag.ListOfDepartments = repository.Departments;

            return View();
        }

        /// <summary>
        /// action method that shows a form that holds data of newly created errand. Passes the errand object to the validation action method
        /// </summary>
        /// <returns>View with object of errand created from form</returns>
        public ViewResult ReportCrime()
        {
            var errand = HttpContext.Session.GetJson<Errand>("NewErrand");
            if (errand == null)
            {
                return View();
            }
            else
            {
                return View(errand);
            }
        }

        /// <summary>
        /// Action Method for View <c>CrimeCoordinator</c> which shows data of one single errand (via a view component: ErrandDetails)
        /// </summary>
        /// <param name="id">id of the errand to fetch from database</param>
        /// <returns>The CrimeCoordinator view (with errand id as sub url).</returns>
        public ViewResult CrimeCoordinator(int id)
        {
            ViewBag.ID = id;
            TempData["Id"] = id;
            ViewBag.ListOfDepartments = repository.Departments;
            return View();
        }
        /// <summary>
        /// Action methods that take the new errand data from form, add it to a session to save the data temporarily
        /// </summary>
        /// <param name="errand">object of errand created from form</param>
        /// <returns>View with object of errand</returns>
        public ViewResult Validate(Errand errand)
        {
            HttpContext.Session.SetJson("NewErrand", errand);
            return View(errand);
        }

        /// <summary>
        /// Action method that gets the object of errand from session and validate that there is data (and not null), 
        /// creates the errand details and add them to the database via method SaveErrand()
        /// errand object gets a sequence from db. errand number with hard-coded initials, default StatusId.
        /// The Method removes the session after saving the errand.
        /// ViewBag.NewErrandRefNumber is to show the created errand ref number in the <c>Thanks()</c> view.
        /// </summary>
        /// <returns>view with the created errand object.</returns>
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
        /// <summary>
        /// Action method to save changes made to an errand. 
        /// </summary>
        /// <param name="errand">object of errand that will be shown and updated</param>
        /// <returns>Redirect to the Errand detail view <C>CrimeCoordinator</C> with the id of the same errand to show changes</returns>
        [HttpPost]
        public IActionResult Save(Errand errand)
        {

            errand.ErrandId = int.Parse(TempData["Id"].ToString());
            if (errand.DepartmentId != "Välj alla")
            {
                repository.UpdateDepartment(errand);
            }

            return RedirectToAction("CrimeCoordinator", new { id = errand.ErrandId });
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
            if (invokeRequest.DepartmentId != null && invokeRequest.DepartmentId != "Välj alla")
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
