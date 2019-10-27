using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnvironmentCrime.Controllers
{
    public class InvestigatorController : Controller
    {
        private IHostingEnvironment environment;

        private IErrandRepository repository;

        public InvestigatorController(IHostingEnvironment env, IErrandRepository repo)
        {
            environment = env;
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
        [HttpPost]
        public async Task<IActionResult> Save(Errand errand, IFormFile document, IFormFile image)
        {
            errand.ErrandId = int.Parse(TempData["Id"].ToString());
            var tempPath = Path.GetTempFileName();

            string dateTime = DateTime.Now.ToString("yymmddhhss");

            if (errand.InvestigatorAction != null)
            {
                repository.UpdateInvestigatorAction(errand);
            }

            if(errand.InvestigatorInfo!=null)
            {
                repository.UpdateInvestigatorInfo(errand);
            }

            if(errand.StatusId!= "Välj")
            {
                repository.UpdateStatusId(errand);
            }

            //Handle document(s) upload
            if (document != null) //skip if no documents are chosen.
            {
                if (document.Length > 0)
                    {
                        using (var stream = new FileStream(tempPath, FileMode.Create))
                        {
                            await document.CopyToAsync(stream);
                        }
                    }
                    //get file extension....then rename file name as caseNo. check if path exists => rename path without extension +(i) and add extension
                    int index = document.FileName.LastIndexOf('.');
                    string fileExt = document.FileName.Substring(index + 1);

                    //Naming file with special format : this is to assure no duplicates
                    var path = Path.Combine(environment.WebRootPath, "uploads/samples", errand.ErrandId + "-doc-" + dateTime + "." + fileExt);
                    System.IO.File.Move(tempPath, path);
            }

            //handle image(s) upload
            if (image.Length > 0)
            {
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }
                //get file extension....then rename file name as caseNo. check if path exists => rename path without extension +(i) and add extension
                int index = image.FileName.LastIndexOf('.');
                string fileExt = image.FileName.Substring(index + 1);

                //Naming file with special format : this is to assure no duplicates
                var path = Path.Combine(environment.WebRootPath, "uploads/images", errand.ErrandId + "-img-" + dateTime + "." + fileExt);
                System.IO.File.Move(tempPath, path);
            }
            
            return RedirectToAction("CrimeInvestigator", new { id = errand.ErrandId });            
        }
    }
}
