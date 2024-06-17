using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace baitapapinetcore.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int DonHangID { get; set; }
        [Required]
        public int SanPhamId { get; set; }
        [Required]
        public string? TenSP { get; set; }
        [Required]
        public double GiaGoc { get; set; }
        [Required]
        public double GiaBan { get; set; }
        [Required]
        public float GiamGia { get; set; }
        [Required]
        public int SoLuong { get; set; }

        [ForeignKey("DonHangID")]
        public virtual Order? Order { get; set; }
    }
}
