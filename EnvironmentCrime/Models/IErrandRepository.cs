using System.Linq;
using System.Threading.Tasks;


namespace EnvironmentCrime.Models
{
    public interface IErrandRepository
    {

        /// <summary>
        /// Interface to be used in the application to fetch data from database, fitler it and apply CRUD operations.
        /// </summary>
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

        string GetUserDepartment();

        IQueryable<ErrandTableItem> GetErrandList(InvokeRequest request);
    }
}
