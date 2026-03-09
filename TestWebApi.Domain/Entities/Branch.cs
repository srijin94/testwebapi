using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWebApi.Domain.Entities
{
    [Table("branches")]
    public class Branch
    {
        [Key]
        [Column("branch_id")]
        public int BranchId { get; private set; }
        [Required]
        [Column("branch_name")]
        public string BranchName { get; private set; }
        [Required]
        [Column("order_no")]
        public int OrderNo { get; private set; }
        private Branch() { }
        public Branch(string branchname,int orderno)
        {
            BranchName = branchname;
            OrderNo = orderno;
        }


    }
}
