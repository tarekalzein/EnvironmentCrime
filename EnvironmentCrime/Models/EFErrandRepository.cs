using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
 /// <summary>
 /// Class to make data from the database available.
 /// </summary>
    public class EFErrandRepository : IErrandRepository
    {
        private ApplicationDbContext context;
        private IHttpContextAccessor contextAccessor;

        /// <summary>
        /// Constructor with connection to application database and ContextAccessor to fetch logged user.
        /// </summary>
        /// <param name="ctx">object of the applicaiton database.</param>
        /// <param name="contextAccessor"></param>
        public EFErrandRepository(ApplicationDbContext ctx, IHttpContextAccessor contextAccessor)
        {
            context = ctx;
            this.contextAccessor = contextAccessor;
        }

        public IQueryable<Errand> Errands => context.Errands.Include(e => e.Samples).Include(e => e.Pictures);

        //.Where(x => x.DepartmentId!="D00") is added to exclude the Småstads Kommun from List
        public IQueryable<Department> Departments => context.Departments.Where(x => x.DepartmentId != "D00");
        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Sequence> Sequences => context.Sequences;

        public IQueryable<Sample> Samples => context.Samples;

        public IQueryable<Picture> Pictures => context.Pictures;

        /// <summary>
        /// Method that fetches errand detail from database
        /// </summary>
        /// <param name="id">Id of the requested errand</param>
        /// <returns>errandDetail model object</returns>
        public Task<Errand> GetErrandDetail(int id)
        {
            return Task.Run(() =>
            {
                var errandDetail = Errands.First(td => td.ErrandId == id);
                return errandDetail;
            });
        }

        /// <summary>
        /// Method to add the errand object data to database if no similar ID exists.
        /// </summary>
        /// <param name="errand">object of errand to be added to to DB</param>
        /// <returns>Created errand's ref number to be shown in view.</returns>
        public string SaveErrand(Errand errand)
        {
            if (errand.ErrandId == 0)
            {
                context.Errands.Add(errand);
            }
            context.SaveChanges();

            return errand.RefNumber;
        }

        /// <summary>
        /// Method to fetch current sequence from the database.
        /// </summary>
        /// <returns>int the current value of the sequence.</returns>
        public int GetSequence()
        {
            var sequenceDetail = Sequences.Where(sq => sq.Id == 1).First();
            return sequenceDetail.CurrentValue;
        }

        /// <summary>
        /// Method to increment the sequence after each new errand.
        /// </summary>
        public void UpdateSequence()
        {
            Sequence dbEntry = context.Sequences.FirstOrDefault(sq => sq.Id == 1);
            dbEntry.CurrentValue += 1;
            context.SaveChanges();
        }
        /// <summary>
        /// Methods to update information of an errand. 
        /// </summary>
        /// <param name="errand"></param>
        /// <returns>The updated errand Id</returns>
        public int UpdateDepartment(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId); //This Can be put in a different method which is called by each update method.
            if (dbEntry != null)
            {
                dbEntry.DepartmentId = errand.DepartmentId;
            }
            context.SaveChanges();
            return errand.ErrandId;
        }
        public int UpdateAction(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if (dbEntry != null)
            {
                dbEntry.StatusId = "S_B";
                dbEntry.InvestigatorInfo = errand.InvestigatorInfo;
                dbEntry.EmployeeId = null;
            }
            context.SaveChanges();
            return errand.ErrandId;
        }

        public int UpdateEmployee(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if (dbEntry != null)
            {
                dbEntry.EmployeeId = errand.EmployeeId;
            }
            context.SaveChanges();
            return errand.ErrandId;
        }

        public int UpdateInvestigatorAction(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if (dbEntry != null)
            {
                if (dbEntry.InvestigatorAction == null)
                {
                    dbEntry.InvestigatorAction = errand.InvestigatorAction;
                }
                else
                {
                    dbEntry.InvestigatorAction += "\n" + errand.InvestigatorAction;
                }
            }
            context.SaveChanges();
            return errand.ErrandId;
        }

        public int UpdateInvestigatorInfo(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if (dbEntry != null)
            {
                if (dbEntry.InvestigatorInfo == null)
                {
                    dbEntry.InvestigatorInfo = errand.InvestigatorInfo;
                }
                else
                {
                    dbEntry.InvestigatorInfo += "\n" + errand.InvestigatorInfo;
                }
            }
            context.SaveChanges();
            return errand.ErrandId;
        }

        public int UpdateStatusId(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId);
            if (dbEntry != null)
            {
                dbEntry.StatusId = errand.StatusId;
            }
            context.SaveChanges();
            return errand.ErrandId;
        }

        /// <summary>
        /// <c>AddPicture</c> and <c>AddSample</c>
        /// Methods to add details of the attached errand to the db.
        /// data includes name of the file and its link (address).
        /// </summary>
        /// <param name="picture"></param>
        public void AddPicture(Picture picture)
        {
            if (picture.PictureId == 0)
            {
                context.Pictures.Add(picture);
            }
            context.SaveChanges();
        }

        public void AddSample(Sample sample)
        {
            if (sample.SampleId == 0)
            {
                context.Samples.Add(sample);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Helper method that returns an employee department ID.
        /// </summary>
        /// <returns></returns>
        public string GetUserDepartment()
        {
            var userId = contextAccessor.HttpContext.User.Identity.Name;
            var employee = Employees.Where(em => em.EmployeeId == userId).First();
            return employee.DepartmentId;

        }

        /// <summary>
        /// Method to fetch list of data, filtered by request.
        /// </summary>
        /// <param name="request">Object of <c>InvokeRequest</c> that holds filter information to fetch data from the database</param>
        /// <returns>returns the generated list.</returns>
        public IQueryable<ErrandTableItem> GetErrandList(InvokeRequest request)
        {
            var tempList = Errands;
            //No matter what, get the employee ID first
            var userId = contextAccessor.HttpContext.User.Identity.Name;

            var employee = Employees.Where(em => em.EmployeeId == userId).First();

            //Filtering data first according to employee's role.
            if (employee.RoleTitle == "Coordinator")
            {
                tempList = Errands; //Create initial List for the specific employee role.

                if (request.RefNumber != null)
                {
                    tempList = tempList.Where(err => err.RefNumber.Contains(request.RefNumber)); // search input doesn't need to be exact.
                    //tempList = tempList.Where(err => err.RefNumber == request.RefNumber);
                }
                if (request.StatusId != null)
                {
                    tempList = tempList.Where(err => err.StatusId == request.StatusId);
                }
                if (request.DepartmentId != null)
                {
                    tempList = tempList.Where(err => err.DepartmentId == request.DepartmentId);
                }

            }
            if (employee.RoleTitle == "Manager")
            {
                tempList = Errands.Where(err => err.DepartmentId == employee.DepartmentId);

                if (request.RefNumber != null)
                {
                    tempList = tempList.Where(err => err.RefNumber.Contains(request.RefNumber));
                }
                if (request.StatusId != null)
                {
                    tempList = tempList.Where(err => err.StatusId == request.StatusId);
                }
                if (request.EmployeeId != null)
                {
                    tempList = tempList.Where(err => err.EmployeeId == request.EmployeeId);
                }
            }
            if (employee.RoleTitle == "Investigator")
            {
                tempList = Errands.Where(err => err.EmployeeId == employee.EmployeeId);

                if (request.RefNumber != null)
                {
                    tempList = tempList.Where(err => err.RefNumber.Contains(request.RefNumber));
                }
                if (request.StatusId != null)
                {
                    tempList = tempList.Where(err => err.StatusId == request.StatusId);
                }
            }

            //Joining tables with the resulting list. This is to show some information in a more user-friendly manner:example: Status name. department name.
            var errandList = from err in tempList
                             join stat in ErrandStatuses on err.StatusId equals stat.StatusId
                             join dep in Departments on err.DepartmentId equals dep.DepartmentId
                             into departmentErrand
                             from deptE in departmentErrand.DefaultIfEmpty()
                             join em in Employees on err.EmployeeId equals em.EmployeeId
                             into employeeErrand
                             from empE in employeeErrand.DefaultIfEmpty()
                             orderby err.RefNumber descending
                             select new ErrandTableItem

                             {
                                 DateOfObservation = err.DateOfObservation,
                                 ErrandId = err.ErrandId,
                                 RefNumber = err.RefNumber,
                                 TypeOfCrime = err.TypeOfCrime,
                                 StatusName = stat.StatusName,
                                 DepartmentName =
                             (err.DepartmentId == null ? "ej tillsatt" : deptE.DepartmentName), //show "ej tillsatt" if no data available in this field.
                                 EmployeeName =
                             (err.EmployeeId == null ? "ej tillsatt" : empE.EmployeeName)
                             };


            return errandList;
        }

    }
}
