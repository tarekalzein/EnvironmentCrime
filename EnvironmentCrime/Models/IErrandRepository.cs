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
        //IQueryable<ErrandTableItem> ErrandTableItems { get; }
        Task<Errand> GetErrandDetail(int id);


        IQueryable<Sample> Samples { get; }

        IQueryable<Picture> Pictures { get; }

        string SaveErrand(Errand errand);

        int GetSequence();

        void UpdateSequence();

        int UpdateDepartment(Errand errand);

        int UpdateEmployee(Errand errand);

        int UpdateAction(Errand errnad);

        int UpdateInvestigatorAction(Errand errand);

        int UpdateInvestigatorInfo(Errand errand);

        int UpdateStatusId(Errand errand);

        void AddPicture(Picture picture);
        void AddSample(Sample sample);
        string GetUserName();

        string GetUserDepartment();

        IQueryable<ErrandTableItem> GetCoorErrandList();
        IQueryable<ErrandTableItem> GetMgrErrandList(string managerDepId);
        IQueryable<ErrandTableItem> GetInvErrandList();
    }
}
