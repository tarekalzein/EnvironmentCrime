using System;

namespace EnvironmentCrime.Models
{
    public class ErrandTableItem
    {
        public DateTime DateOfObservation { get; set; }
        public int ErrandId { get; set; }
        public string RefNumber { get; set; }
        public string TypeOfCrime { get; set; }
        public string StatusName { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
    }
}
