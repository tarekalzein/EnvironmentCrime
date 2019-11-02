using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
    public class StatusList : ViewComponent
    {
        IErrandRepository repository;

        public StatusList(IErrandRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.ListOfStatuses = repository.ErrandStatuses;
            return View();
        }
    }
}
