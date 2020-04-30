using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProfileViewModels
{
    public class ProfileDetailsViewModel
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Payment Types")]
        public List<PaymentType> PaymentTypes { get; set; }

        [Display(Name = "Order History")]
        public List<Order> Orders { get; set; }

        [Display(Name = "Products")]
        public List<Product> Products { get; set; }


    }
}
