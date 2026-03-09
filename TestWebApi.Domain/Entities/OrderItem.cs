using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Domain.Entities
{
    [Table("order_items")]
    public class OrderItem
    {
        [Key]
        [Column("trans_id")]
        public int TransId { get; set; }

        [Required]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        // Navigation property
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
    }
}
