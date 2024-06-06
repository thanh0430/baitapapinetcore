using baitapapinetcore.Models;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace baitapapinetcore.Services.AccountSevice
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AccountRepository(MyDbContext dbContext, IConfiguration configuration) 
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

       public async Task<ViewAccount> AddAccount(ViewAccount ViewAccount)
       {
            if (!IsValidEmail(ViewAccount.Email))
            {
                throw new Exception("Email chưa đúng định dạng.");
            }
            var newAccount = new Account
            {
                FirstName = ViewAccount.FirstName,
                LastName = ViewAccount.LastName,
                Address = ViewAccount.Address,
                SCCCD = ViewAccount.SCCCD,
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = ViewAccount.Role,
            };
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            return new ViewAccount
            {
                Id = newAccount.Id,
                FirstName = newAccount.FirstName,
                LastName = newAccount.LastName,
                Address = newAccount.Address,
                SCCCD = newAccount.SCCCD,
                Email = newAccount.Email,
                Password = newAccount.Password,
                Role = newAccount.Role,
            };
        }

        public async  Task DeleteAsync(int id)
        {
            var reesuilt = await _dbContext.Accounts.SingleOrDefaultAsync(x => x.Id == id);
            if(reesuilt != null)
            {
                _dbContext.Accounts.Remove(reesuilt);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("ID không tồn tại");
            }
        } 

        public async Task<List<ViewAccount>>GetAllAsync()
        {
            return await _dbContext.Accounts
                .Select(x => new ViewAccount
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    SCCCD = x.SCCCD,
                    Email = x.Email,
                    Password = x.Password,
                    Role = x.Role,

                }).ToListAsync();
        }

        public async Task<ViewAccount> GetByIdAsync(int id)
        {
            var resuilt = await _dbContext.Accounts.SingleOrDefaultAsync(p => p.Id == id);
            if(resuilt == null)
            {
                throw new Exception("ID không tồn tại");
            }
            else
            {
                return new ViewAccount { 
                    Id = resuilt.Id,
                    FirstName = resuilt.FirstName,
                    LastName = resuilt.LastName,
                    Address = resuilt.Address,
                    SCCCD= resuilt.SCCCD,
                    Email = resuilt.Email,
                    Password = resuilt.Password,
                    Role = resuilt.Role,
                };

            }
        }

        public async Task UpdateAsync(ViewAccount viewAccount, int id)
        {
            var result = await _dbContext.Accounts.SingleOrDefaultAsync(p => p.Id == id);
            if (result == null)
            {
                throw new Exception("Bạn đã nhập sai ID");
            }
            else
            {

                result.LastName = viewAccount.LastName;
                result.FirstName = viewAccount.FirstName;
                result.Address = viewAccount.Address;
                result.SCCCD = viewAccount.SCCCD;

                if (!IsValidEmail(viewAccount.Email))
                {
                    throw new Exception("Email chưa đúng định dạng.");
                }

                result.Email = viewAccount.Email;
                result.Password = viewAccount.Password;
                result.Role = viewAccount.Role;

                await _dbContext.SaveChangesAsync();
            }
        }
       
        public async Task<string> LoginAsync(ViewAccount ViewAccount)
        {
            var authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, ViewAccount.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, ViewAccount.Role)
            };

            var authenkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:ValidIssuer"],
                audience: _configuration["jwt:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authenClaims,
                signingCredentials: new SigningCredentials(authenkey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> LoginAdmin(ViewAccount ViewAccount)
        {
            if (!IsValidEmail(ViewAccount.Email))
            {
                throw new Exception("Email chưa đúng định dạng.");
            }
            var user = await _dbContext.Accounts.SingleOrDefaultAsync(p => p.Email == ViewAccount.Email && p.Role == "Admin");
            if (user == null || user.Password != ViewAccount.Password)
            {
                throw new ArgumentException("Bạn đã nhập sai tài khoản hoặc mật khẩu");
            }
            var viewAccount = new ViewAccount
            {
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "Admin"
            };

            var token = await LoginAsync(viewAccount);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Đăng nhập không thành công.");
            }

            return token;
        }

        public async Task<string> LoginUser(ViewAccount ViewAccount)
        {
            if (!IsValidEmail(ViewAccount.Email))
            {
                throw new Exception("Email chưa đúng định dạng.");
            }
            var user = await _dbContext.Accounts.SingleOrDefaultAsync(p => p.Email == ViewAccount.Email && p.Role == "User");
            if (user == null || user.Password != ViewAccount.Password)
            {
                throw new ArgumentException("Bạn đã nhập sai tài khoản hoặc mật khẩu");
            }
            var viewAccount = new ViewAccount
            {
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "User"
            };

            var token = await LoginAsync(viewAccount);
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Đăng nhập không thành công.");
            }

            return token;
        }

        public async Task<ViewAccount> RegisterUserAsync(ViewAccount ViewAccount)
        {
            if (!IsValidEmail(ViewAccount.Email))
            {
                throw new Exception("Email chưa đúng định dạng.");
            }
            var newAccount = new Account
            {
                FirstName = ViewAccount.FirstName,
                LastName = ViewAccount.LastName,
                Address = ViewAccount.Address,
                SCCCD = ViewAccount.SCCCD,
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "User",
            };
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            return new ViewAccount
            {
                Id = newAccount.Id,
                FirstName = newAccount.FirstName,
                LastName = newAccount.LastName,
                Address = newAccount.Address,
                SCCCD = newAccount.SCCCD,
                Email = newAccount.Email,
                Password = newAccount.Password,
                Role = newAccount.Role,
            };

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
