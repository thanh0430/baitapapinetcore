namespace baitapapinetcore.ViewModels
{
    public class ViewVoucherForAcc
    {
        public int id { get; set; }
        public int voucherId { get; set; }
        public int UserID { get; set; }
        public DateTime? RedeemedDate { get; set; } 
        public int RedeemedStatus { get; set; }
    }
}
