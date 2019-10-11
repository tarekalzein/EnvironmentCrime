using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
    public class EFErrandRepository : IErrandRepository
    {
        private ApplicationDbContext context;

        public EFErrandRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Errand> Errands => context.Errands;
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.First(td => td.ErrandId == id);
                return errandDetail;
            });
        }
    }
}
