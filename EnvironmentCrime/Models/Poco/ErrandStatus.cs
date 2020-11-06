using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
    public class ErrandStatus
    {
        [Key]
        public string StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
