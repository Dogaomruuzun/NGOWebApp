﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NGOAppMVC.DBModels
{
    public partial class Volunteer
    {
        public long NgouserId { get; set; }
        public long? RegionId { get; set; }
        public long? ProfessionId { get; set; }
        public long? AnnualIncome { get; set; }
        public long? AvailableDaysCount { get; set; }
        public long? AvailableHoursCount { get; set; }
        public long? CanHandleOwnTransportation { get; set; }

        public virtual Ngouser Ngouser { get; set; }
        public virtual LkpProfession Profession { get; set; }
    }
}