using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypeViewModels
{
    public class AllCategoryProductsViewModel
    {
        [Key]
        public int ptId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Category")]
        public string Label { get; set; }
        public Product Product { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
