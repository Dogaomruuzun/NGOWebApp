using Microsoft.AspNetCore.Mvc.Rendering;
using NGOAppMVC.DBModels;

namespace NGOAppMVC.Models
{
    public class DTOWarehouseAssets
    {
        public long? Id { get; set; }
        public long? DonationId { get; set; }
        public long? ImportedVolunteerId { get; set; }
        public string ImportDate { get; set; }
        public long? ExportedVolunteerId { get; set; }
        public string ExportDate { get; set; }
        public long? IndigentId { get; set; }
        public List<SelectListItem> VolunteerList { get; set; }
        public DTODonations Donation { get; set; }
    }
}
