using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnvironmentCrime.Models;

namespace EnvironmentCrime.Components
{
    public class ErrandDetail : ViewComponent
    {
        private IErrandRepository repository;

        public ErrandDetail(IErrandRepository repo)
        {
            repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var objectOfErrand = await repository.GetErrandDetail(id);
            return View(objectOfErrand);
        }
    }
}
