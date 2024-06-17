using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace baitapapinetcore.Models
{
    [Table("Voucher")]
    public class Voucher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? VoucherCode {  get; set; }
        public string? VoucherName { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public string? VoucherType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status  { get; set; }
        public int IdCreator { get; set; }// người tạo Voucher

        [ForeignKey("IdCreator")]
        public virtual Account? Account { get; set; }
    }
}
