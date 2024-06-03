using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.ProductService
{
    public interface IProductRepository
    {
        Task<List<ViewProducts>> GetAllAsync();
        Task<ViewProducts> GetByIdAsync(int id);
        Task<ViewProducts> AddAsync(ViewProducts viewProduct);
        Task UpdateAsync(ViewProducts viewProduct);
        Task DeleteAsync(int id);
    }
}
