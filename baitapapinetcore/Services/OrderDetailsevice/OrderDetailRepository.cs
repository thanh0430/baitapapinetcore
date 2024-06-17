using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.OrderDetailsevice
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly MyDbContext _dbContext;

        public OrderDetailRepository( MyDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<ViewOrderDetail> AddAsync(ViewOrderDetail ViewOrderDetail)
        {
            var newOrderDetail = new OrderDetail 
            { 
                DonHangID = ViewOrderDetail.DonHangID,
                GiaBan = ViewOrderDetail.GiaBan,
                GiaGoc = ViewOrderDetail.GiaGoc,
                GiamGia = ViewOrderDetail.GiamGia,
                SanPhamId = ViewOrderDetail.SanPhamId,
                SoLuong = ViewOrderDetail.SoLuong,
                TenSP = ViewOrderDetail.TenSP
            };
            await _dbContext.AddAsync(ViewOrderDetail);
            await _dbContext.SaveChangesAsync();
            return new ViewOrderDetail
            {
                Id = newOrderDetail.Id,
                DonHangID = newOrderDetail.DonHangID,
                GiaBan = newOrderDetail.GiaBan,
                GiaGoc = newOrderDetail.GiaGoc,
                GiamGia = newOrderDetail.GiamGia,
                SanPhamId = newOrderDetail.SanPhamId,
                SoLuong = newOrderDetail.SoLuong,
                TenSP = newOrderDetail.TenSP
            };
        }

        public async Task DeleteAsync(int id)
        {
            var resuilt = await _dbContext.OrderDetails.SingleOrDefaultAsync(p => p.Id == id);
            if(resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                 _dbContext.OrderDetails.Remove(resuilt);
                await _dbContext.SaveChangesAsync(); 
            }
        }

        public async Task<List<ViewOrderDetail>> GetAllAsync()
        {
            return await _dbContext.OrderDetails
                .Select(sl => new ViewOrderDetail
                { 
                    Id = sl.Id,
                    DonHangID = sl.DonHangID,
                    GiaBan = sl.GiaBan,
                    GiaGoc = sl.GiaGoc,
                    GiamGia = sl.GiamGia,
                    SanPhamId = sl.SanPhamId,
                    SoLuong = sl.SoLuong,
                    TenSP = sl.TenSP                   
                }).ToListAsync();
        }

        public async Task<ViewOrderDetail> GetByIdAsync(int id)
        {
            var resuilt = await _dbContext.OrderDetails.SingleOrDefaultAsync(p => p.Id == id);
            if (resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                return new ViewOrderDetail
                {
                    Id = resuilt.Id,
                    DonHangID = resuilt.DonHangID,
                    GiaBan = resuilt.GiaBan,
                    GiaGoc = resuilt.GiaGoc,
                    GiamGia = resuilt.GiamGia,
                    SanPhamId = resuilt.SanPhamId,
                    SoLuong = resuilt.SoLuong,
                    TenSP = resuilt.TenSP
                };
            }
        }
        public async Task UpdateAsync(ViewOrderDetail ViewOrderDetail, int id)
        {
            var resuilt = await _dbContext.OrderDetails.SingleOrDefaultAsync(p => p.Id == id);
            if(resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {             
                resuilt.Id = ViewOrderDetail.Id;
                resuilt.DonHangID = ViewOrderDetail.DonHangID;
                resuilt.GiaBan = ViewOrderDetail.GiaBan;
                resuilt.GiaGoc = ViewOrderDetail.GiaGoc;
                resuilt.GiamGia = ViewOrderDetail.GiamGia;
                resuilt.SanPhamId = ViewOrderDetail.SanPhamId;
                resuilt.SoLuong = ViewOrderDetail.SoLuong;
                resuilt.TenSP = ViewOrderDetail.TenSP;
           }
        }
    }
}
