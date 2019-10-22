using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace EnvironmentCrime.Models
{
    public interface IErrandRepository
    {
        IQueryable<Errand> Errands { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<Employee> Employees { get; }
        IQueryable<ErrandStatus> ErrandStatuses { get; }
        Task<Errand> GetErrandDetail(int id);

        string SaveErrand(Errand errand);

        int GetSequence();

        void UpdateSequence();
    }
}
