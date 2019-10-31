using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentCrime.Models
{
    public class InvokeRequest
    {
        public string StatusId { get;set; }

        public string DepartmentId { get; set; }
        
        public string RefNumber { get; set; }

        public string EmployeeId { get; set; }
    }
}
