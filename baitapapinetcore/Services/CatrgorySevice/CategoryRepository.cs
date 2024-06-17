using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.CatrgorySevice
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _dbcontext;

        public CategoryRepository(MyDbContext dbContext) 
        { 
            _dbcontext = dbContext;
        }

        public async Task<ViewCategory> AddAsync(ViewCategory ViewCategory, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                throw new Exception("file không tồn tại");
            }

            string extension = Path.GetExtension(file.FileName);
            string fileName = DateTime.Now.ToString("yyyyMMddssffff") + extension;
            string filePath = Path.Combine(@"E:\baitapapinetcore\baitapapinetcore\Image\", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            ViewCategory.Image = fileName;
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
                Id = newCategory.Id,
                producttype = newCategory.producttype,
                CategoryName = newCategory.CategoryName,
                Image = newCategory.Image
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

        public async Task UpdateAsync(ViewCategory ViewCategory, int id, IFormFile file)
        {
            var result = await _dbcontext.Categories.SingleOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {
                throw new Exception("Không tồn tại ID");
            }

            if (file != null && file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                string fileName = DateTime.Now.ToString("yyyyMMddssffff") + extension;
                string filePath = Path.Combine(@"E:\baitapapinetcore\baitapapinetcore\Image\", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                ViewCategory.Image = fileName;
            }

            result.CategoryName = ViewCategory.CategoryName;
            result.Image = ViewCategory.Image ?? result.Image;
            result.producttype = ViewCategory.producttype;

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<ViewCategorywithproduct> GetCategoryWithProductsExplicitly(int id)
        {
            var category = await _dbcontext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("Không tồn tại ID");
            }


            _dbcontext.Entry(category).Collection(c => c.Products).Load();

            var viewCategory = new ViewCategorywithproduct
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

        public async Task<List<ViewCategorywithproduct>> GetCategoryWithProducts()
        {
            
            var categories = await _dbcontext.Categories
                .Include(c => c.Products)                
                .ToListAsync();

            if (categories == null)
            {
                throw new Exception("Không tồn tại danh mục nào");
            } 
            var viewCategories = categories.Select(category => new ViewCategorywithproduct
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

        public async Task<ViewCategorywithproduct> GetCategoryWithProductsLazyloading(int id)
        {
            var category = await _dbcontext.Categories.FindAsync(id);

            if (category == null)
            {
                throw new Exception("Không tìm thấy danh mục có ID tương ứng.");
            }

            // Chuyển đổi đối tượng Category thành đối tượng ViewCategory

            var viewCategory = new ViewCategorywithproduct
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
