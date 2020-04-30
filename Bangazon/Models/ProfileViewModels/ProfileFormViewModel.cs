using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProfileViewModels
{
    public class ProfileFormViewModel
    {

        [Key]
        public string UserId { get; set; }

 
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

 
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

  
    }
}
