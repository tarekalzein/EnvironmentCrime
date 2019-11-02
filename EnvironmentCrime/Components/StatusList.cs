using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

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
