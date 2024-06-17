using baitapapinetcore.ViewModels;
using System.Collections.Generic;

namespace baitapapinetcore.Services.OrderSevice
{
    public interface IOrderRepository
    {
        Task<List<ViewOrder>> GetAllAsync();
        Task<ViewOrder> GetByIdAsync(int id);
        Task<List<CreateOrderRequest>> AddAsync(List <CreateOrderRequest> createOrderRequest);
        Task UpdateAsync(ViewOrder ViewOrder, int id);
        Task DeleteAsync(int id);
        Task <ViewOrderWithOrderDetail>GetOrderWithOrderDetail(int id);
        Task<BaoCao> GetOrderby();
        Task <List<BieuDo>> GetOrderchart();
        Task <List<Thongke>> GetOrderbystatus();
        Task <List<ViewProducts>> GetGellinPproducts();
        Task<List<ViewOrder>> GetLatestProduct();

    }
}
