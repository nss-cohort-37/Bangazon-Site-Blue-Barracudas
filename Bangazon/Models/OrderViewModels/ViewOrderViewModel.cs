using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.OrderViewModels
{
    public class ViewOrderViewModel
    {
        public Order Order { get; set; }
        public double TotalPrice
        {
            get
            {

                double _totalPrice = 0;

                if (Order.OrderProducts != null)
                {

                    foreach (var product in Order.OrderProducts)
                    {
                        _totalPrice += product.Product.Price;
                    }

                }

                return _totalPrice;
            }
        }
    }
}