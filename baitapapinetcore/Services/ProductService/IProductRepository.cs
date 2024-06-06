using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.ProductService
{
    public interface IProductRepository
    {
        Task<List<ViewProducts>> GetAllAsync();
        Task<ViewProducts> GetByIdAsync(int id);
        Task<ViewProducts> AddAsync(ViewProducts viewProduct, IFormFile file);
        Task UpdateAsync(ViewProducts viewProduct, IFormFile file, int id);
        Task DeleteAsync(int id);
    }
}
