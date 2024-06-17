using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace baitapapinetcore.Models
{
    [Table("VoucherForAcc")]
    public class VoucherForAcc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id {  get; set; }
        public int voucherId { get; set; }
        public int UserID {  get; set; }
        public DateTime? RedeemedDate { get; set; } // ngày khách hàng sử dụng voucher
        public int RedeemedStatus { get; set; } // người dùng đã sử dụng voucher chưa 0: chưa, 1: rồi
        [ForeignKey("voucherId")]
        public virtual Voucher? Voucher { get; set; }  
    }
}
