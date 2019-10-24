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

        //.Where(x => x.DepartmentId!="D00") is added to exclude the Småstads Kommun from List
        public IQueryable<Department> Departments => context.Departments.Where(x => x.DepartmentId!="D00");
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

        public int UpdateErrand(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if(dbEntry!=null)
            {
                dbEntry.DepartmentId = errand.DepartmentId;
            }
            context.SaveChanges();
            return errand.ErrandId;
        }
        



    }
}
