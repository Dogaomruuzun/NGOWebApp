
using Microsoft.AspNetCore.Mvc.Rendering;
using NGOAppMVC.DBModels;
using System.ComponentModel.DataAnnotations;

namespace NGOAppMVC.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Age")]
        [Range(0, 150, ErrorMessage = "Please enter valid age")]
        public long? Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Boolean? Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public string PhoneNumber { get; set; }

        public long? VolunteerRegionId { get; set; }
        public long? VolunteerProfessionId { get; set; }
        public long? VolunteerAnnualIncome { get; set; }

        [Range(0, 150, ErrorMessage = "Please enter valid number")]
        public long? VolunteerAvailableDaysCount { get; set; }
        [Range(0, 150, ErrorMessage = "Please enter valid number")]
        public long? VolunteerAvailableHoursCount { get; set; }
        public Boolean? VolunteerCanHandleOwnTransportation { get; set; }

        public long IndigentMonthlyIncome { get; set; }
        public long? IndigentMonthlyExpenditures { get; set; }
        public long? IndigentRegionId { get; set; }

        public List<DTOIndigentDependents> Dependents { get; set; }
        public List<SelectListItem> EducationList { get; set; }
        public List<SelectListItem> ProfessionList { get; set; }
        public List<SelectListItem> GeographicalList { get; set; }
        public List<SelectListItem> RelationList { get; set; }
        public List<SelectListItem> EmploymentStatusList { get; set; }

    }
}
