using System.ComponentModel.DataAnnotations;

namespace Bangazon.Models {
    public class OrderProduct {
        [Key]
        public int OrderProductId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public Product Product { get; set; }

    }
}