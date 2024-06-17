using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.VoucherService
{
    public interface IVoucherRepository
    {
        Task<List<ViewVoucher>> GetAll();
        Task<ViewVoucher> GetById(int id);
        Task<ViewVoucher> CreateVoucher(ViewVoucher ViewVoucher);
        Task UpdateVoucher(ViewVoucher ViewVoucher, int id);
        Task DeleteVoucher(int id);
    }
}
