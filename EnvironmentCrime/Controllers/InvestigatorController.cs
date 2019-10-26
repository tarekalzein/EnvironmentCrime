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
            return View(repository);
        }

        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ID = id;
            TempData["Id"] = id;
            ViewBag.ListOfStatuses = repository.ErrandStatuses;
            return View();
        }

        //TEST ACTION METHOD
        //public ViewResult Save(Errand errand)
        //{
        //    errand.ErrandId = int.Parse(TempData["Id"].ToString());

        //    ViewBag.TestErrandId = errand.ErrandId;
        //    ViewBag.TestInvestigatorAction = errand.InvestigatorAction;
        //    ViewBag.TestInvestigatorInfo = errand.InvestigatorInfo;
        //    ViewBag.TestStatusId = errand.StatusId;

        //    return View();
        //}

        public IActionResult Save(Errand errand)
        {
            errand.ErrandId = int.Parse(TempData["Id"].ToString());

            if (errand.InvestigatorAction != null)
            {
                repository.UpdateInvestigatorAction(errand);
            }

            if(errand.InvestigatorInfo!=null)
            {
                repository.UpdateInvestigatorInfo(errand);
                //update InvestigatorInfo
            }

            if(errand.StatusId!= "Välj")
            {
                repository.UpdateStatusId(errand);
                //Update StatusId
            }

            return RedirectToAction("CrimeInvestigator", new { id = errand.ErrandId });
        }
    }
}
