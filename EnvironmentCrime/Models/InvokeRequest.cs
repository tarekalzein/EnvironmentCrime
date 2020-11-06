namespace EnvironmentCrime.Models
{
    public class InvokeRequest
    {

        /// <summary>
        /// Singleton class to hold the filtering criterias for filtering errand tables
        /// according to role and search parameters (and dropdown lists).
        /// </summary>
        public string StatusId { get; set; }

        public string DepartmentId { get; set; }

        public string RefNumber { get; set; }

        public string EmployeeId { get; set; }
    }
}
