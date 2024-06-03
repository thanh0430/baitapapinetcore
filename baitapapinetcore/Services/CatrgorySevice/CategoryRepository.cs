using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace baitapapinetcore.Services.CatrgorySevice
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _dbcontext;

        public CategoryRepository(MyDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }

        public async Task<ViewCategory> AddAsync(ViewCategory ViewCategory)
        {
            var newCategory = new Category
            {
                producttype = ViewCategory.producttype,
                CategoryName = ViewCategory.CategoryName,
                Image = ViewCategory.Image
            };
            _dbcontext.Categories.Add(newCategory);
            await _dbcontext.SaveChangesAsync();

            return new ViewCategory
            {
                
                CategoryName = newCategory.CategoryName,
                Image = newCategory.Image,
                producttype = newCategory.producttype

            };
        }

        public async Task DeleteAsync(int id)
        {
            var resuilt = await _dbcontext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (resuilt != null)
            {
                _dbcontext.Categories.Remove(resuilt);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<List<ViewCategory>> GetAllAsync()
        {

            return await _dbcontext.Categories
                .Select(sl => new ViewCategory              
                { 
                    Id = sl.Id,
                    CategoryName = sl.CategoryName,
                    Image = sl.Image,
                    producttype = sl.producttype
                }).ToListAsync();
        }

        public async Task<ViewCategory> GetByIdAsync(int id)
        {
            var resuilt = await _dbcontext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (resuilt == null)
            {
                throw new Exception("Không tồn tại ID");
            }
            else
            {
                return new ViewCategory
                {
                    Id = resuilt.Id,
                    producttype = resuilt.producttype,
                    CategoryName = resuilt.CategoryName,
                    Image = resuilt.Image
                };
            }
        }

        public async Task UpdateAsync(ViewCategory ViewCategory)
        {
            var resuilt = await _dbcontext.Categories.SingleOrDefaultAsync(p =>p.Id == ViewCategory.Id);
            if (resuilt == null)
            {
                throw new Exception("Không tồn tại ID");
            }
            else
            {
                resuilt .CategoryName = ViewCategory.CategoryName;
                resuilt.Image = ViewCategory.Image;
                resuilt.producttype = ViewCategory.producttype;
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<ViewCategory> GetCategoryWithProductsExplicitly(int id)
        {
            var category = await _dbcontext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("Không tồn tại ID");
            }

            // Explicitly load the related products
            _dbcontext.Entry(category).Collection(c => c.Products).Load();

            var viewCategory = new ViewCategory
            {
                Id = category.Id,
                producttype = category.producttype,
                CategoryName = category.CategoryName,
                Image = category.Image,
                Products = category.Products.Select(p => new ViewProducts
                {
                    Id = p.Id,
                    TenSanPham = p.TenSanPham,
                    GiaBan = p.GiaBan,
                    Anh = p.Anh,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    CategoryID = p.CategoryID,
                    SoLuong = p.SoLuong
                }).ToList()
            };

            return viewCategory;
        }

        public async Task<List<ViewCategory>> GetCategoryWithProducts()
        {
            
            var categories = await _dbcontext.Categories
                .Include(c => c.Products)
                    .ThenInclude(p => p.Category) // Include thông tin liên quan của sản phẩm
                .ToListAsync();

            if (categories == null)
            {
                throw new Exception("Không tồn tại danh mục nào");
            }

            // Trả về danh sách các đối tượng ViewCategory
            var viewCategories = categories.Select(category => new ViewCategory
            {
                Id = category.Id,
                producttype = category.producttype,
                CategoryName = category.CategoryName,
                Image = category.Image,
                Products = category.Products.Select(p => new ViewProducts
                {
                    Id = p.Id,
                    TenSanPham = p.TenSanPham,
                    GiaBan = p.GiaBan,
                    Anh = p.Anh,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    CategoryID = p.CategoryID,
                    SoLuong = p.SoLuong
                }).ToList()
            }).ToList();

            return viewCategories;
        }

        public async Task<ViewCategory> GetCategoryWithProductsLazyloading(int id)
        {
            var category = await _dbcontext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Không tìm thấy danh mục có ID tương ứng.");
            }

            // Chuyển đổi đối tượng Category thành đối tượng ViewCategory
            var viewCategory = new ViewCategory
            {
                Id = category.Id,
                producttype = category.producttype,
                CategoryName = category.CategoryName,
                Image = category.Image,
                Products = category.Products.Select(p => new ViewProducts
                {
                    Id = p.Id,
                    TenSanPham = p.TenSanPham,
                    GiaBan = p.GiaBan,
                    Anh = p.Anh,
                    TrangThai = p.TrangThai,
                    MoTa = p.MoTa,
                    CategoryID = p.CategoryID,
                    SoLuong = p.SoLuong
                }).ToList()
            };

            return viewCategory;
        }
    }
}
