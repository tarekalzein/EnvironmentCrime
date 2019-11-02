using EnvironmentCrime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
