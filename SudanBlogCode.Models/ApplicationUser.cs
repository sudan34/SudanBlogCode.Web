using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudanBlogCode.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public String Name { get; set; }
        public String MobileNumber { get; set; }
    }
}
