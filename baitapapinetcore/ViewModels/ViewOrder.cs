﻿namespace baitapapinetcore.ViewModels
{
    public class ViewOrder
    {
        public int ID { get; set; }
        public string KhachHangSDT { get; set; }
        public DateTime NgayBan { get; set; }
        public double TongTien { get; set; }
        public int TrangThai { get; set; }
        public string TenKH { get; set; }
        public string DiaChi { get; set; }
       // public List<ViewProducts>? Products { get; set; }
    }
}
