namespace baitapapinetcore.ViewModels
{
    public class CreateOrderRequest
    {
        public int ID { get; set; }
        public string? KhachHangSDT { get; set; }
        public DateTime NgayBan { get; set; }
        public double TongTien { get; set; }
        public int TrangThai { get; set; }
        public string? TenKH { get; set; }
        public string? DiaChi { get; set; }


        public int DonHangID { get; set; }
        public int SanPhamId { get; set; }
        public string? TenSP { get; set; }
        public double GiaGoc { get; set; }
        public double GiaBan { get; set; }
        public float GiamGia { get; set; }
        public int SoLuong { get; set; }
    }
}
