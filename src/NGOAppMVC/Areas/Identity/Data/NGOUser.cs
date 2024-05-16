using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NGOAppMVC.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the NGOUser class
    public class NGOUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
