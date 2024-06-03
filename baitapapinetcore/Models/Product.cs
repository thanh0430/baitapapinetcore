using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required]
        public string TenSanPham { get; set; }
        [Required]
        public double GiaBan { get; set; }
        [Required]
        public string Anh { get; set; }
        [Required]
        public int TrangThai { get; set; }
        [Required]
        public string MoTa { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int SoLuong { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }
    }
}
