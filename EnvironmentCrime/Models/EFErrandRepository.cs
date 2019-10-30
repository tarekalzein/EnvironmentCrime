﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
    public class EFErrandRepository : IErrandRepository
    {
        private ApplicationDbContext context;
        private IHttpContextAccessor contextAccessor;

        public EFErrandRepository(ApplicationDbContext ctx, IHttpContextAccessor contextAccessor)
        {
            context = ctx;
            this.contextAccessor = contextAccessor;
        }

        public IQueryable<Errand> Errands => context.Errands.Include(e=>e.Samples).Include(e=>e.Pictures);

        //.Where(x => x.DepartmentId!="D00") is added to exclude the Småstads Kommun from List
        public IQueryable<Department> Departments => context.Departments.Where(x => x.DepartmentId!="D00");
        public IQueryable<ErrandStatus> ErrandStatuses => context.ErrandStatuses;
        public IQueryable<Employee> Employees => context.Employees;
        public IQueryable<Sequence> Sequences => context.Sequences;

        public IQueryable<Sample> Samples => context.Samples;

        public IQueryable<Picture> Pictures => context.Pictures;

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

        public int UpdateDepartment(Errand errand)
        {
            Errand dbEntry = context.Errands.FirstOrDefault(s => s.ErrandId == errand.ErrandId); //This Can be put in a different method which is called by each update method.
            if(dbEntry!=null)
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
                dbEntry.StatusId="S_B";
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
                }else
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

        public string GetUserName()
        {
            var userId = contextAccessor.HttpContext.User.Identity.Name;
            return userId;
        }

        public string GetUserDepartment()
        {
            string  userId = GetUserName();
            var employee = Employees.Where(em => em.EmployeeId == userId).First();


            return employee.DepartmentId;
           
        }
    }
}
