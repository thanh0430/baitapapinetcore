using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.ProductService
{
    public class ProductRepository : IProductRepository//GenericRepository<Product>, IRepository<Product>
    {
        public readonly MyDbContext _context;
        public ProductRepository(MyDbContext context) 
        {
            _context =  context;
        }

        public async Task<ViewProducts> AddAsync(ViewProducts viewProduct)
        {
            var newProduct = new Product
            {
                TenSanPham = viewProduct.TenSanPham,
                GiaBan = viewProduct.GiaBan,
                Anh = viewProduct.Anh,
                MoTa = viewProduct.MoTa,
                CategoryID = viewProduct.CategoryID,
                SoLuong = viewProduct.SoLuong,
                TrangThai = viewProduct.TrangThai,
            };
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return new ViewProducts
            {
                Id = newProduct.Id,
                TenSanPham = newProduct.TenSanPham,
                GiaBan = newProduct.GiaBan,
                Anh = newProduct.Anh,
                MoTa = newProduct.MoTa,
                CategoryID = newProduct.CategoryID,
                SoLuong = newProduct.SoLuong,
                TrangThai = newProduct.TrangThai,
            };
        }


        public async Task DeleteAsync(int id)
        {
            var resuilt = await _context.Products.SingleOrDefaultAsync(p=> p.Id == id);
            if(resuilt == null)
            {
                throw new Exception("không tồn tại ID");
            }
            else 
            { 
                 _context.Products.Remove(resuilt);
                _context.SaveChanges();
            }
        }

        public async Task<List<ViewProducts>> GetAllAsync()
        {
            return await _context.Products
              .Select(sl => new ViewProducts
              {
                  Id= sl.Id,
                  TenSanPham = sl.TenSanPham,
                  GiaBan = sl.GiaBan,
                  Anh = sl.Anh,
                  MoTa = sl.MoTa,
                  CategoryID = sl.CategoryID,
                  SoLuong = sl.SoLuong,
                  TrangThai = sl.TrangThai,
              }).ToListAsync();

        }

        public async Task<ViewProducts> GetByIdAsync(int id)
        {
            var resuilt = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (resuilt == null)
            {
                throw new Exception("không tồn tại ID");

            }
            else
            {
                return new ViewProducts
                {
                    Id = resuilt.Id,
                    TenSanPham = resuilt.TenSanPham,
                    GiaBan = resuilt.GiaBan,
                    Anh = resuilt.Anh,
                    MoTa = resuilt.MoTa,
                    CategoryID = resuilt.CategoryID,
                    SoLuong = resuilt.SoLuong,
                    TrangThai = resuilt.TrangThai,
                };
            }
        }

        public async Task UpdateAsync(ViewProducts viewProduct)
        {
            var resuilt = await _context.Products.SingleOrDefaultAsync(p => p.Id == viewProduct.Id);
            if (resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == viewProduct.CategoryID);
            if (!categoryExists)
            {
                throw new Exception("ID danh mục không tồn tại");
            }

            resuilt.TenSanPham = viewProduct.TenSanPham;
            resuilt.GiaBan = viewProduct.GiaBan;
            resuilt.Anh = viewProduct.Anh;
            resuilt.MoTa = viewProduct.MoTa;
            resuilt.CategoryID = viewProduct.CategoryID;
            resuilt.SoLuong = viewProduct.SoLuong;
            resuilt.TrangThai = viewProduct.TrangThai;

            await _context.SaveChangesAsync();
        }

    }
}
