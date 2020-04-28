using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypeViewModels
{
    public class TypeWithProducts
    {
        [Key]
        public int TypeId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Category")]
        public string TypeName { get; set; }

        [NotMapped]
        public int ProductCount { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
