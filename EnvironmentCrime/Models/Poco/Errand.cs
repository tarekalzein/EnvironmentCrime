using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EnvironmentCrime.Models
{
    public class Errand
    {
        public string ErrandID { get; set; }
        [Required(ErrorMessage ="Du måste fylla i platsen där brottet ha skett")]
        [StringLength(30, MinimumLength = 3,ErrorMessage = "Plats: ange minst 3 bokstäver och max 30")]
        public string Place { get; set; }
        [Required(ErrorMessage = "Du måste fylla typ av brott")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Typ av brott: ange minst 3 bokstäver och max 30")]
        public string TypeOfCrime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}")]
        [Required(ErrorMessage = "Du måste fylla i Datum för observation")]
        public DateTime DateOfObservation { get; set; }
        public string Observation { get; set; }
        public string InvestigatorInfo { get; set; }
        public string InvestigatorAction { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ditt namn")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Namn: ange minst 3 bokstäver och max 30")]
        public string InformerName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ditt telefonnummer")]
        [RegularExpression(@"^[0]{1}[0-9]{1,3}-[0-9]{5,9}$",ErrorMessage ="telefonnummers format måste vara: xxx-xxxxxxxxx")]
        public string InformerPhone { get; set;}
        public string StatusId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }


    }
}
