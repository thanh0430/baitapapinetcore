using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.ViewModels
{
    public class ViewOrderDetail
    {
        public int Id { get; set; }
        public int DonHangID { get; set; }
        public int SanPhamId { get; set; }
        public string? TenSP { get; set; }
        public double GiaGoc { get; set; }
        public double GiaBan { get; set; }
        public float GiamGia { get; set; }
        public int SoLuong { get; set; }
    }
}
