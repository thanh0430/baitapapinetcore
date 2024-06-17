using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ViewProducts> AddAsync(ViewProducts viewProduct, IFormFile file)
        {
            string fname = DateTime.Now.ToString("yyyyMMddssffff") + "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
           
                if (file.Length > 0)
                {

                    string path2 = Path.Combine(@"E:\baitapapinetcore\baitapapinetcore\Image\" + fname);
                    using (var stream2 = System.IO.File.Create(path2))
                    {
                        file.CopyTo(stream2);
                    }
                    viewProduct.Anh = fname;
                }            
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
        public async Task UpdateAsync(ViewProducts viewProduct, IFormFile file, int id)
        {
            var result = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {
                throw new ArgumentException("Không tồn tại ID", nameof(id));
            }

            if (file != null && file.Length > 0)
            {

                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}_{file.FileName}";

                try
                {
                    string path = Path.Combine(@"E:\baitapapinetcore\baitapapinetcore\Image", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    viewProduct.Anh = fileName;
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu gặp phải vấn đề khi lưu file
                    throw new Exception("Lỗi khi lưu file", ex);
                }
            }
            result.TenSanPham = viewProduct.TenSanPham;
            result.GiaBan = viewProduct.GiaBan;
            result.MoTa = viewProduct.MoTa;
            result.CategoryID = viewProduct.CategoryID;
            result.SoLuong = viewProduct.SoLuong;
            result.TrangThai = viewProduct.TrangThai;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin sản phẩm", ex);
            }
        }

    }
}
