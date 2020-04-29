using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderPaymentFormViewModel
    {
        public int OrderId { get; set; }

        public int PaymentTypeId { get; set; }
        public List<SelectListItem> PaymentTypeOptions { get; set; }
    }
}
