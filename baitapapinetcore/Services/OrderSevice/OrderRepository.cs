using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.OrderSevice
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _dbContext;

        public OrderRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CreateOrderRequest>> AddAsync(List<CreateOrderRequest> createOrderRequests)
        {
            if (createOrderRequests == null || createOrderRequests.Count == 0)
            {
                throw new Exception("Không có yêu cầu thanh toán nào được cung cấp");
            }

            var addedOrders = new List<CreateOrderRequest>();

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var newOrder = new Order
                {
                    KhachHangSDT = createOrderRequests.First().KhachHangSDT,
                    DiaChi = createOrderRequests.First().DiaChi,
                    TenKH = createOrderRequests.First().TenKH,
                    NgayBan = createOrderRequests.First().NgayBan,
                    TongTien = createOrderRequests.First().TongTien,
                    TrangThai = createOrderRequests.First().TrangThai,
                };

                _dbContext.Orders.Add(newOrder);
                await _dbContext.SaveChangesAsync();

                foreach (var item in createOrderRequests)
                {
                    var product = await _dbContext.Products.FindAsync(item.SanPhamId);
                    if (product == null)
                    {
                        throw new Exception($"Sản phẩm với ID {item.SanPhamId} không tồn tại");
                    }

                    if (product.SoLuong < item.SoLuong)
                    {
                        throw new Exception($"Sản phẩm {item.TenSP} không đủ số lượng trong kho");
                    }

                    var orderDetail = new OrderDetail
                    {
                        DonHangID = newOrder.ID,
                        SanPhamId = item.SanPhamId,
                        TenSP = item.TenSP,
                        GiaGoc = item.GiaGoc,
                        GiaBan = item.GiaBan,
                        GiamGia = item.GiamGia,
                        SoLuong = item.SoLuong
                    };

                    _dbContext.OrderDetails.Add(orderDetail);

                    product.SoLuong -= item.SoLuong;
                    _dbContext.Products.Update(product);
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                addedOrders.Add(createOrderRequests.First());
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Đã xảy ra lỗi khi xử lý đơn hàng: {ex.Message}");
            }

            return addedOrders;
        }
        public async Task DeleteAsync(int id)
        {
            var resuilt = await _dbContext.Orders.SingleOrDefaultAsync(p => p.ID == id);
            if (resuilt == null)
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

        public async Task<BaoCao> GetOrderby()
        {
            if (_dbContext.Orders == null)
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }
            var baocao = new BaoCao();
            var list = await _dbContext.Orders.ToListAsync();
            baocao.chuaTT = list.Where(e => e.TrangThai == 2).Count();
            baocao.doanhthu = list.Where(e => e.TrangThai == 2).Sum(e => e.TongTien);
            baocao.sodonhang = list.Count;
            baocao.TongDHhuy = list.Where(e => e.TrangThai == 3).Count();
            return baocao;
        }

        public async Task<List<Thongke>> GetOrderbystatus()
        {
            if (_dbContext.Orders == null)
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }
            Thongke thongke = new Thongke();
            Thongke thongke1 = new Thongke();
            Thongke thongke2 = new Thongke();
            Thongke thongke3 = new Thongke();
            List<Thongke> tk = new List<Thongke>();
            var list = await _dbContext.Orders.ToListAsync();
            thongke.Name = "Chưa thanh toán";
            thongke.Value = list.Where(e => e.TrangThai == 0).Count();
            tk.Add(thongke);

            thongke1.Name = "Đã thanh toán ";
            thongke1.Value = list.Where(e => e.TrangThai == 1).Count();
            tk.Add(thongke1);

            thongke2.Name = "Thành công";
            thongke2.Value = list.Where(e => e.TrangThai == 2).Count();
            tk.Add(thongke2);

            thongke3.Name = "Bị Hủy";
            thongke3.Value = list.Where(e => e.TrangThai == 3).Count();
            tk.Add(thongke3);

            return tk;
        }

        public async Task<List<BieuDo>> GetOrderchart()
        {
            if (_dbContext.Orders == null)
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }

            var oneYearAgo = DateTime.Now.AddMonths(-12);

            var revenueByMonth = await _dbContext.Orders
                .Where(dh => dh.NgayBan >= oneYearAgo)
                .Select(dh => new
                {
                    Thang = dh.NgayBan.Month,
                    Nam = dh.NgayBan.Year,
                    TongDoanhThu = dh.TongTien,
                    Trangthai = dh.TrangThai,
                })
                .GroupBy(r => new { r.Thang, r.Nam })
                .Select(g => new
                {
                    Thang = g.Key.Thang,
                    Nam = g.Key.Nam,
                    name = $"T{g.Key.Thang}",
                    doanhthu = g.Sum(r => r.TongDoanhThu)/1000,
                    SoLuongDonHang = g.Where(e => e.Trangthai == 3).Sum(r => r.TongDoanhThu)/1000
                })
                .OrderBy(r => r.Nam)
                .ThenBy(r => r.Thang)
                .ToListAsync();

            var result = revenueByMonth.Select(g => new BieuDo
            {
                doanhthu = g.doanhthu,
                sotienhuy = g.SoLuongDonHang,
                name = g.name
            }).ToList();

            // Fill in the missing months
            for (int i = result.Count ; i <= 12; i++)
            {
                BieuDo a = new BieuDo();
                DateTime date = DateTime.Now.AddMonths(-i);
                a.name = "T" + date.Month.ToString();
                a.sotienhuy = 0;
                a.doanhthu = 0;
                result.Add(a);
            }
            return result;
        }

        public async Task<ViewOrderWithOrderDetail> GetOrderWithOrderDetail(int id)
        {
            var order = await _dbContext.Orders
                .Include(od => od.OrderDetail)
                .FirstOrDefaultAsync(o => o.ID == id);

            if (order == null)
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }

            var viewOrder = new ViewOrderWithOrderDetail
            {
                ID = order.ID,
                KhachHangSDT = order.KhachHangSDT,
                DiaChi = order.DiaChi,
                TenKH = order.TenKH,
                NgayBan = order.NgayBan,
                TongTien = order.TongTien,
                TrangThai = order.TrangThai,
                OrderDetail = order.OrderDetail.Select(od => new ViewOrderDetail
                {
                    Id = od.Id,
                    DonHangID = od.DonHangID,
                    GiaBan = od.GiaBan,
                    GiaGoc = od.GiaGoc,
                    GiamGia = od.GiamGia,
                    SanPhamId = od.SanPhamId,
                    SoLuong = od.SoLuong,
                    TenSP = od.TenSP
                }).ToList()
            };

            return viewOrder;
        }

        public async Task UpdateAsync(ViewOrder ViewOrder, int id)
        {
            var resuilt = await _dbContext.Orders.SingleOrDefaultAsync(p => p.ID == id);
            if (resuilt == null)
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

        public async Task<List<ViewProducts>> GetGellinPproducts()
        {
            if (_dbContext.Orders == null)
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }

            var listchitiet = await _dbContext.OrderDetails
                                    .Select(e => new { SanPhamId = e.SanPhamId, TongSoLuong = e.SoLuong })
                                    .GroupBy(e => e.SanPhamId)
                                    .Select(g => new
                                    {
                                        SanPhamId = g.Key,
                                        TongSoLuong = g.Sum(x => x.TongSoLuong)
                                    })
                                    .ToListAsync();
            var listsanpham = await _dbContext.Products.ToListAsync();
            var joinedProducts = (from product in listsanpham
                                  join detail in listchitiet on product.Id equals detail.SanPhamId
                                  select new ViewProducts
                                  {
                                      Id = product.Id,
                                      TenSanPham = product.TenSanPham, 
                                      GiaBan = product.GiaBan, 
                                      Anh = product.Anh, 
                                      TrangThai = product.TrangThai, 
                                      MoTa = product.MoTa, 
                                      CategoryID = product.CategoryID, 
                                      SoLuong = detail.TongSoLuong 
                                  }).ToList();
            return joinedProducts;
        }

        public async Task<List<ViewOrder>> GetLatestProduct()
        {
            if (!_dbContext.Orders.Any())
            {
                throw new Exception("Không tồn tại đơn hàng nào");
            }

            var latestOrders = await _dbContext.Orders
                .OrderByDescending(e => e.NgayBan)
                .Take(6)
                .ToListAsync();

            var viewOrders = latestOrders.Select(order => new ViewOrder
            {
                ID = order.ID,
                KhachHangSDT = order.KhachHangSDT,
                NgayBan = order.NgayBan,
                TongTien = order.TongTien,
                TrangThai = order.TrangThai,
                TenKH = order.TenKH,
                DiaChi = order.DiaChi
            }).ToList();

            return viewOrders;
        }

    }
}

