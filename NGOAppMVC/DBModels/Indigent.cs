﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NGOAppMVC.DBModels
{
    public partial class Indigent
    {
        public long MonthlyIncome { get; set; }
        public long NgouserId { get; set; }
        public long? MonthlyExpenditures { get; set; }
        public long? DonableItemId { get; set; }
        public long? RegionId { get; set; }

        public virtual LkpDonableItem DonableItem { get; set; }
        public virtual Ngouser Ngouser { get; set; }
        public virtual LkpRegions Region { get; set; }
    }
}