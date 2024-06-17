using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace baitapapinetcore.Services.VoucherService
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly MyDbContext _dbContext;

        public VoucherRepository(MyDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<ViewVoucher> CreateVoucher(ViewVoucher viewVoucher)
        {
            var newVoucher = new Voucher
            {
                VoucherName = viewVoucher.VoucherName,
                VoucherType = viewVoucher.VoucherType,
                VoucherCode = viewVoucher.VoucherCode,
                CreationDate = viewVoucher.CreationDate,
                ExpirationDate = viewVoucher.ExpirationDate,
                Description = viewVoucher.Description,
                IdCreator = viewVoucher.IdCreator,
                Status = viewVoucher.Status,
                Value = viewVoucher.Value
            };

            await _dbContext.AddAsync(newVoucher);
            await _dbContext.SaveChangesAsync();

            return new ViewVoucher
            {
                Id = newVoucher.Id,
                VoucherName = newVoucher.VoucherName,
                VoucherType = newVoucher.VoucherType,
                VoucherCode = newVoucher.VoucherCode,
                CreationDate = newVoucher.CreationDate,
                ExpirationDate = newVoucher.ExpirationDate,
                Description = newVoucher.Description,
                IdCreator = newVoucher.IdCreator,
                Status = newVoucher.Status,
                Value = newVoucher.Value,
            };
        }
        public async Task DeleteVoucher(int id)
        {
            var resuilt = await _dbContext.Vouchers.SingleOrDefaultAsync(x => x.Id == id);
            if (resuilt == null)
            {
                throw new Exception("Id not found");
            }
            else
            {
                _dbContext.Vouchers.Remove(resuilt);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<ViewVoucher>> GetAll()
        {
            return await _dbContext.Vouchers
                .Select(sl => new ViewVoucher
                {
                    Id = sl.Id,
                    VoucherName = sl.VoucherName,
                    VoucherType = sl.VoucherType,
                    VoucherCode = sl.VoucherCode,
                    CreationDate = sl.CreationDate,
                    ExpirationDate = sl.ExpirationDate,
                    Description = sl.Description,
                    IdCreator = sl.IdCreator,
                    Status = sl.Status,
                    Value = sl.Value, 
                }).ToListAsync();
        }

        public async Task<ViewVoucher> GetById(int id)
        {
            var resuilt = await _dbContext.Vouchers.SingleOrDefaultAsync(p => p.Id == id);
            if (resuilt == null)
            {
                throw new Exception("Id not found");
            }
            else
            {
                return new ViewVoucher
                {
                    Id = resuilt.Id,
                    VoucherName = resuilt.VoucherName,
                    VoucherType = resuilt.VoucherType,
                    VoucherCode = resuilt.VoucherCode,
                    CreationDate = resuilt.CreationDate,
                    ExpirationDate = resuilt.ExpirationDate,
                    Description = resuilt.Description,
                    IdCreator = resuilt.IdCreator,
                    Status = resuilt.Status,
                    Value = resuilt.Value,
                };
            }
        }

        public async Task UpdateVoucher(ViewVoucher ViewVoucher, int id)
        {
            var resuilt = await _dbContext.Vouchers.SingleOrDefaultAsync(p => p.Id == id);
            if ( resuilt == null)
            {
                throw new Exception("Id not found");
            }
            else
            {            
                resuilt.VoucherName = ViewVoucher.VoucherName;
                resuilt.VoucherType = ViewVoucher.VoucherType;
                resuilt.VoucherCode = ViewVoucher.VoucherCode;
                resuilt.CreationDate = ViewVoucher.CreationDate;
                resuilt.ExpirationDate = ViewVoucher.ExpirationDate;
                resuilt.Description = ViewVoucher.Description;
                resuilt.IdCreator = ViewVoucher.IdCreator;
                resuilt.Status = ViewVoucher.Status;
                resuilt.Value = ViewVoucher.Value;
            }
        }
    }
}
