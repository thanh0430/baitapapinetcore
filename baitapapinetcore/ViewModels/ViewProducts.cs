using System.ComponentModel.DataAnnotations;

namespace baitapapinetcore.ViewModels
{
    public class ViewProducts
    {
        public int? Id { get; set; }
        public string TenSanPham { get; set; }
        public double GiaBan { get; set; }
        public string Anh { get; set; }
        public int TrangThai { get; set; }
        public string MoTa { get; set; }
        public int CategoryID { get; set; }
        public int SoLuong { get; set; }
    }
}
