using baitapapinetcore.Models;
using baitapapinetcore.Services.SendEmailSevice;
using baitapapinetcore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace baitapapinetcore.Services.AccountSevice
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly SendEmailSevice.SendEmailSevice _sendEmail;

        #region
        public AccountRepository(MyDbContext dbContext, IConfiguration configuration, SendEmailSevice.SendEmailSevice sendEmail) 
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _sendEmail = sendEmail;
        }
        #endregion

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
                PhoneNumber = ViewAccount.PhoneNumber,
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = ViewAccount.Role,
            };
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            await _sendEmail.SendEmail(new EmailViewModel
            {
                ToEmail = newAccount.Email,
                Subject = "Xác nhận đăng ký tài khoản",
                Body = $"Chào mừng bạn đến với ứng dụng của chúng tôi! Tài khoản của bạn đã được đăng ký thành công."
            });
            return new ViewAccount
            {
                Id = newAccount.Id,
                FirstName = newAccount.FirstName,
                LastName = newAccount.LastName,
                Address = newAccount.Address,
                PhoneNumber= newAccount.PhoneNumber,
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
                    PhoneNumber = x.PhoneNumber,
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
                    PhoneNumber= resuilt.PhoneNumber,
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
                result.PhoneNumber = viewAccount.PhoneNumber;

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

        public async Task<string> LoginAsync(ViewAccount viewAccount, bool includeAdditionalClaims = false)
        {          
            var authenClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, viewAccount.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, viewAccount.Role),
                new Claim("Name", $"{viewAccount.FirstName} {viewAccount.LastName}"),
            };

            if (includeAdditionalClaims)
            {
                authenClaims.Add(new Claim("PhoneNumber", viewAccount.PhoneNumber));
                authenClaims.Add(new Claim("Address", viewAccount.Address));
            }

            var authenkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:ValidIssuer"],
                audience: _configuration["jwt:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
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
       
            var token = await LoginAsync(new ViewAccount
            {
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "Admin",
            });

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

            var token = await LoginAsync(new ViewAccount
            {
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "User",
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            }, true);

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
                PhoneNumber = ViewAccount.PhoneNumber,
                Email = ViewAccount.Email,
                Password = ViewAccount.Password,
                Role = "User",
            };
            if (_dbContext.Accounts.Any(a => a.Email == ViewAccount.Email))
            {
                throw new Exception("Email đã tồn tại");
            }
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.SaveChangesAsync();
            await _sendEmail.SendEmail(new EmailViewModel
            {
                ToEmail = newAccount.Email,
                Subject = "Xác nhận đăng ký tài khoản",
                Body = $"Chào mừng bạn đến với ứng dụng của chúng tôi! Tài khoản của bạn đã được đăng ký thành công."
            });

            return new ViewAccount
            {
                Id = newAccount.Id,
                FirstName = newAccount.FirstName,
                LastName = newAccount.LastName,
                Address = newAccount.Address,
                SCCCD = newAccount.SCCCD,
                PhoneNumber = newAccount.PhoneNumber,
                Email = newAccount.Email, 
                Password = newAccount.Password,
                Role = newAccount.Role,
            };

        }
        //public async Task<ViewAccount> AuthenticateGoogleUserAsync(ClaimsPrincipal principal)
        //{
        //    var googleId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var user = await _dbContext.Accounts.FirstOrDefaultAsync(u => u.GoogleId == googleId);

        //    if (user == null)
        //    {
        //        var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        //        var name = principal.FindFirst(ClaimTypes.Name)?.Value;
        //        var avatar = principal.FindFirst("urn:google:picture")?.Value;

        //        var newAccount = new Account
        //        {
        //            GoogleId = googleId,
        //            Email = email,
        //            LastName = name,
        //            Avatar = avatar,
        //        };
        //        _dbContext.Accounts.Add(newAccount);
        //        await _dbContext.SaveChangesAsync();

        //        user = newAccount;
        //    }

        //    var viewAccount = new ViewAccount
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Address = user.Address,
        //        SCCCD = user.SCCCD,
        //        PhoneNumber = user.PhoneNumber,
        //        GoogleId = user.GoogleId,
        //        Avatar = user.Avatar,
        //        Email = user.Email,
        //        Password = user.Password,
        //        Role = user.Role
        //    };

        //    return viewAccount;
        //}

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

       public async Task<string>GetUserRole(string email, string password)
        {
            var user = await _dbContext.Accounts.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user?.Role;
        }
    }
}
