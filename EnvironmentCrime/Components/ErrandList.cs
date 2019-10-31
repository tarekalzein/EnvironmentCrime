using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
    public class ErrandList : ViewComponent
    {

        private IErrandRepository repository;

        public ErrandList(IErrandRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var errandList = await repository.GetCoorErrandList();

            return View(errandList);
        }



    }
}
