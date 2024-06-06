using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using System;

namespace baitapapinetcore.Services.CatrgorySevice
{
    public interface ICategoryRepository
    {
        Task<List<ViewCategory>> GetAllAsync();
        Task<ViewCategory> GetByIdAsync(int id);
        Task<ViewCategory> AddAsync(ViewCategory ViewCategory, IFormFile file);
        Task UpdateAsync(ViewCategory ViewCategory, int id, IFormFile file);
        Task DeleteAsync(int id);
        Task <List<ViewCategorywithproduct>> GetCategoryWithProducts();
        Task<ViewCategorywithproduct> GetCategoryWithProductsLazyloading(int id);
        Task<ViewCategorywithproduct> GetCategoryWithProductsExplicitly(int id);
    }
}
