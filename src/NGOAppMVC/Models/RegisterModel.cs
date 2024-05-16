
using NGOAppMVC.DBModels;
using System.ComponentModel.DataAnnotations;

namespace NGOAppMVC.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long? Age { get; set; }
        public long? Gender { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
        public string PhoneNumber { get; set; }

        public long? VolunteerRegionId { get; set; }
        public long? VolunteerProfessionId { get; set; }
        public long? VolunteerAnnualIncome { get; set; }
        public long? VolunteerAvailableDaysCount { get; set; }
        public long? VolunteerAvailableHoursCount { get; set; }
        public long? VolunteerCanHandleOwnTransportation { get; set; }

        public long IndigentMonthlyIncome { get; set; }
        public long? IndigentMonthlyExpenditures { get; set; }
        public long? IndigentRegionId { get; set; }

        public List<DTOIndigentDependents> Dependents { get; set; }
        public List<DTOLkpEducationalStatus> EducationalStatusList { get; private set; }

        //create DTO objects for the folder od DbModels and use them here


    }
}
