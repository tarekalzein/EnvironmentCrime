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
        public IQueryable<Department> Departments => context.Departments;
        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Sequence> Sequences => context.Sequences;
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.First(td => td.ErrandId == id);
                return errandDetail;
            });
        }

        public string SaveErrand (Errand errand)
        {
            if(errand.ErrandId==0)
            {
                context.Errands.Add(errand);
            }
            context.SaveChanges();

            return errand.RefNumber;
        }

        public int GetSequence()
        {
            var sequenceDetail = Sequences.Where(sq => sq.Id == 1).First();
            return sequenceDetail.CurrentValue;
        }
        public void UpdateSequence()
        {
            Sequence dbEntry = context.Sequences.FirstOrDefault(sq => sq.Id == 1);
            dbEntry.CurrentValue += 1;
            context.SaveChanges();
        }
        



    }
}
