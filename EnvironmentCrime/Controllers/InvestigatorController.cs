using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace EnvironmentCrime.Controllers
{
    [Authorize(Roles = "Investigator")]
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
        public ViewResult StartInvestigator(InvokeRequest request)
        {
            ViewBag.ErrandList = repository.GetErrandList(request);
            return View();
        }

        public ViewResult CrimeInvestigator(int id)
        {
            ViewBag.ID = id;
            TempData["Id"] = id;
            ViewBag.ListOfStatuses = repository.ErrandStatuses;
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Save(Errand errand, IFormFile document, IFormFile image)
        {
            errand.ErrandId = int.Parse(TempData["Id"].ToString());
            var tempPath = Path.GetTempFileName();

            string dateTime = DateTime.Now.ToString("yyMMddHHmmss");

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
                string docFileName = (errand.ErrandId + "-doc-" + dateTime + "." + fileExt);

                    //Naming file with special format : this is to assure no duplicates
                    var path = Path.Combine(environment.WebRootPath, "uploads/samples", docFileName);
                    System.IO.File.Move(tempPath, path);
                Sample sample = new Sample();
                sample.SampleName = docFileName;
                sample.ErrandId = errand.ErrandId;
                repository.AddSample(sample);
            }

            //handle image(s) upload
            if (image != null) { 
                if (image.Length > 0)
                {
                using (var stream = new FileStream(tempPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                //get file extension....then rename file name as caseNo. check if path exists => rename path without extension +(i) and add extension
                int index = image.FileName.LastIndexOf('.');
                string fileExt = image.FileName.Substring(index + 1);
                string imgFileName= (errand.ErrandId + "-img-" + dateTime + "." + fileExt);

                //Naming file with special format : this is to assure no duplicates
                var path = Path.Combine(environment.WebRootPath, "uploads/images", imgFileName);
                System.IO.File.Move(tempPath, path);

                Picture picture = new Picture();
                picture.PictureName = imgFileName;
                picture.ErrandId = errand.ErrandId;
                repository.AddPicture(picture);
                }
            }
            return RedirectToAction("CrimeInvestigator", new { id = errand.ErrandId });            
        }

        public IActionResult Filter(InvokeRequest invokeRequest)
        {
            InvokeRequest request = new InvokeRequest { };
            if (invokeRequest.StatusId != null && invokeRequest.StatusId != "Välj alla")
            {
                request.StatusId = invokeRequest.StatusId;
            }
            if (!string.IsNullOrWhiteSpace(invokeRequest.RefNumber))
            {
                request.RefNumber = invokeRequest.RefNumber;
            }

            return RedirectToAction("StartInvestigator", request);
        }
    }
}
