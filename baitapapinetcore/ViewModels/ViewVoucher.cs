namespace baitapapinetcore.ViewModels
{
    public class ViewVoucher
    {
        public int Id { get; set; }
        public string? VoucherCode { get; set; }
        public string? VoucherName { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
        public string? VoucherType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int Status { get; set; }
        public int IdCreator { get; set; }// người tạo Voucher
    }
}
