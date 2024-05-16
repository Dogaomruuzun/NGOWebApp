using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.Areas.Identity.Data;

namespace NGOAppMVC.Data
{
    public class NGOAppMVCContext : IdentityDbContext<NGOUser>
    {
        public NGOAppMVCContext(DbContextOptions<NGOAppMVCContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<NGOUser>(b =>
            {
                b.ToTable("NGOUser"); // Use your custom table name here
                b.Ignore(e => e.UserName);
                b.Ignore(e => e.NormalizedUserName);
                b.Ignore(e => e.NormalizedEmail);
                b.Ignore(e => e.EmailConfirmed);
                b.Ignore(e => e.SecurityStamp);
                b.Ignore(e => e.ConcurrencyStamp);
                b.Ignore(e => e.PhoneNumber);
                b.Ignore(e => e.PhoneNumberConfirmed);
                b.Ignore(e => e.TwoFactorEnabled);
                b.Ignore(e => e.LockoutEnd);
                b.Ignore(e => e.LockoutEnabled);
                b.Ignore(e => e.AccessFailedCount);
            });

        }
    }
}
