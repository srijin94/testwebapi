using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Domain.Entities
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Required]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("order_no")]
        public int OrderNo { get; set; }

        // Navigation property
        public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
