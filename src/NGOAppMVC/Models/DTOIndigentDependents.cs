namespace NGOAppMVC.Models
{
    public class DTOIndigentDependents
    {
        public long Id { get; set; }
        public long? NgouserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? EmploymentStatusId { get; set; }
        public long? EducationStatusId { get; set; }
        public long? DependentRelationId { get; set; }

        public string EmploymentStatusName { get; set; }
        public string EducationStatusName { get; set; }
        public string DependentRelationName { get; set; }

    }
}
