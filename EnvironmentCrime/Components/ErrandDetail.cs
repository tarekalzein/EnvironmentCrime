using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EnvironmentCrime.Components
{
 
    ///ViewComponent class: It contains the details of one single errand.
    public class ErrandDetail : ViewComponent
    {
        private IErrandRepository repository;

        /// <summary>
        /// Constructor with connection to the EF db repository
        /// </summary>
        /// <param name="repo">EF Repo</param>
        public ErrandDetail(IErrandRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Method <c>InvokeAsync</c> that fetches the data of one single errand from DB using its id
        /// and forward the errand using its model to the viewcomponenet
        /// </summary>
        /// <param name="id"> Errand Id</param>
        /// <returns></returns>
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var objectOfErrand = await repository.GetErrandDetail(id);
            return View(objectOfErrand);
        }
    }
}
