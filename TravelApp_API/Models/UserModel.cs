using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TravelApp_API.Models
{
    public class UserModel : IdentityUser
    {
        [Required]
        [Key]
        public override string UserName { get; set; }

        [Required]
        [Display(Name="Password")]
        public override string PasswordHash { get; set; }

     }  
}