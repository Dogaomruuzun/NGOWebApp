using System.ComponentModel;

namespace NGOAppMVC.Models
{
    public class DTODonations
    {
        public long Id { get; set; }
        public long NgouserId { get; set; }
        [DisplayName("Tarih")]
        public string DonationDate { get; set; }
        public long DonableItemId { get; set; }
        [DisplayName("Miktar")]
        public long DonableItemAmount { get; set; }
        public long? RegionId { get; set; }
        [DisplayName("Bağış Türü")]
        public string DonableItem { get; set; }
        [DisplayName("Bölge")]
        public string Region { get; set; }
        [DisplayName("Bağış Yapan")]
        public string NGOUserName { get; set; }
        [DisplayName("Durumu")]
        public string Status { get; set; }


    }
}
