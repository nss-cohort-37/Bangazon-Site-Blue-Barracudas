using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductViewModels
{
    public class ProductFormViewModel
    {
        public int ProductId { get; set; }

        [MaxLength(4097152, ErrorMessage = "File too large")]
        public IFormFile File { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime DateCreated { get; set; }

        [Required]
        [StringLength(255)]
        [RegularExpression(@"^[a-zA-Z0-9'' '.]+$", ErrorMessage = "Special character should not be entered")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9'' ']+$", ErrorMessage = "Special character should not be entered")]
        [StringLength(55, ErrorMessage = "Please shorten the product title to 55 characters")]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(typeof(double), "0.01", "10000.00", ErrorMessage = "Price must be be at least $0.01 and not greater than $10,000.00")]


        public double Price { get; set; } 

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string UserId { get; set; }

        public string City { get; set; }

        public string ImagePath { get; set; }

        public bool Active { get; set; }

        public bool localDelivery { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Product Category")]
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; }

        public List<SelectListItem> ProductTypeOptions { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

        public ProductFormViewModel()
        {
            Active = true;
        }

     

    }
}
