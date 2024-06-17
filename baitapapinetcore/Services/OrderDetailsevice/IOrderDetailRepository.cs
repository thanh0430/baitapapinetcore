using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.OrderDetailsevice
{
    public interface IOrderDetailRepository
    {
        Task<List<ViewOrderDetail>> GetAllAsync();
        Task<ViewOrderDetail> GetByIdAsync(int id);
        Task<ViewOrderDetail> AddAsync(ViewOrderDetail ViewOrderDetail);
        Task UpdateAsync(ViewOrderDetail ViewOrderDetail, int id);
        Task DeleteAsync(int id);
 

        //Task<ViewCategorywithproduct> GetCategoryWithProductsLazyloading(int id);
        //Task<ViewCategorywithproduct> GetCategoryWithProductsExplicitly(int id);
    }
}
