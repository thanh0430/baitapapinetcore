using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.OrderSevice
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _dbContext;

        public OrderRepository( MyDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<ViewOrder>AddAsync(ViewOrder ViewOrder)
        {
            var newOrder = new Order
            {
                ID = ViewOrder.ID,
                KhachHangSDT = ViewOrder.KhachHangSDT,
                DiaChi = ViewOrder.DiaChi,
                TenKH = ViewOrder.TenKH,
                NgayBan = ViewOrder.NgayBan,
                TongTien = ViewOrder.TongTien,
                TrangThai = ViewOrder.TrangThai,
            };
            _dbContext.Orders.Add(newOrder);
            _dbContext.SaveChanges();
            return new ViewOrder
            {
                ID = newOrder.ID,
                KhachHangSDT = newOrder.KhachHangSDT,
                DiaChi = newOrder.DiaChi,
                TenKH = newOrder.TenKH,
                NgayBan = newOrder.NgayBan,
                TongTien = newOrder.TongTien,
                TrangThai = newOrder.TrangThai,
            };
        }

        public async Task DeleteAsync(int id)
        {
            var resuilt = await _dbContext.Orders.SingleOrDefaultAsync(p => p.ID == id);
            if(resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                _dbContext.Orders.Remove(resuilt);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<ViewOrder>> GetAllAsync()
        {
           return await _dbContext.Orders
                .Select(sl => new ViewOrder
                {
                    ID = sl.ID,
                    KhachHangSDT = sl.KhachHangSDT,
                    DiaChi = sl.DiaChi,
                    TenKH = sl.TenKH,
                    NgayBan = sl.NgayBan,
                    TongTien = sl.TongTien,
                    TrangThai = sl.TrangThai,

                }).ToListAsync();
        }

        public async Task<ViewOrder> GetByIdAsync(int id)
        {
            var resuilt = await _dbContext.Orders.SingleOrDefaultAsync(x => x.ID == id);
            if (resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                return new ViewOrder 
                {
                    ID = resuilt.ID,
                    KhachHangSDT = resuilt.KhachHangSDT,
                    DiaChi = resuilt.DiaChi,
                    TenKH = resuilt.TenKH,
                    NgayBan = resuilt.NgayBan,
                    TongTien = resuilt.TongTien,
                    TrangThai = resuilt.TrangThai,
                };
            }
        }
        public async Task UpdateAsync(ViewOrder ViewOrder, int id)
        {
           var resuilt = await _dbContext.Orders.SingleOrDefaultAsync(p => p.ID == id);
            if(resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                resuilt.TenKH = ViewOrder.TenKH;
                resuilt.DiaChi = ViewOrder.DiaChi;
                resuilt.KhachHangSDT = ViewOrder.KhachHangSDT;
                resuilt.NgayBan = ViewOrder.NgayBan;
                resuilt.TrangThai = ViewOrder.TrangThai;
                resuilt.TongTien = ViewOrder.TongTien;
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
