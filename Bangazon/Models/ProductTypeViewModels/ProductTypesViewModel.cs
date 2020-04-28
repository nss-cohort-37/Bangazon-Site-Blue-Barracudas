using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypeViewModels
{
    public class ProductTypesViewModel
    {
        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Category")]
        public string Label { get; set; }

        [NotMapped]
        public int Quantity { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<TypeWithProducts> Types { get; set; }
    }
}
