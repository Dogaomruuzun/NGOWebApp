﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace NGOAppMVC.DBModels
{
    public partial class LkpApprovementStatus
    {
        public LkpApprovementStatus()
        {
            Ngouser = new HashSet<Ngouser>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ngouser> Ngouser { get; set; }
    }
}