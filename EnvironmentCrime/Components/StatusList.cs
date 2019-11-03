using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvironmentCrime.Components
{
    /// <summary>
    /// Class to fetch list of all status states of errands.
    /// </summary>
    public class StatusList : ViewComponent
    {
        IErrandRepository repository;
        /// <summary>
        /// Constructor with connection to the EF repository
        /// </summary>
        /// <param name="repository"></param>
        public StatusList(IErrandRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Method <c>Invoke</c> to fetch the list ErrandStatuses and pass it as Viewbag to the View
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            ViewBag.ListOfStatuses = repository.ErrandStatuses;
            return View();
        }
    }
}
