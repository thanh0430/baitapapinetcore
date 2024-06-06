using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.OrderSevice
{
    public interface IOrderRepository
    {
        Task<List<ViewOrder>> GetAllAsync();
        Task<ViewOrder> GetByIdAsync(int id);
        Task<ViewOrder> AddAsync(ViewOrder ViewOrder);
        Task UpdateAsync(ViewOrder ViewOrder, int id);
        Task DeleteAsync(int id);
    }
}
