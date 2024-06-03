using baitapapinetcore.ViewModels;
using System;

namespace baitapapinetcore.Services.CatrgorySevice
{
    public interface ICategoryRepository
    {
        Task<List<ViewCategory>> GetAllAsync();
        Task<ViewCategory> GetByIdAsync(int id);
        Task<ViewCategory> AddAsync(ViewCategory ViewCategory);
        Task UpdateAsync(ViewCategory ViewCategory);
        Task DeleteAsync(int id);
        Task <List<ViewCategory>> GetCategoryWithProducts();
        Task<ViewCategory> GetCategoryWithProductsLazyloading(int id);
        Task<ViewCategory> GetCategoryWithProductsExplicitly(int id);
    }
}
