using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bangazon.Models.PaymentTypeViewModels
{
    public class PaymentTypeFormViewModel
    {
        public int PaymentTypeId { get; set; }

        public string AccountNumber { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public List<string> Descriptions { get; set; }

        public List<SelectListItem> PaymentTypeOptions { get; set; }
    }
}