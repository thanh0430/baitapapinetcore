using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.AccountSevice
{
    public interface IAccountRepository
    {
        Task<List<ViewAccount>> GetAllAsync();
        Task<ViewAccount> GetByIdAsync(int id);
        Task<ViewAccount> AddAccount(ViewAccount ViewAccount);
        Task UpdateAsync(ViewAccount ViewAccount, int id);
        Task DeleteAsync(int id);
        Task<ViewAccount> RegisterUserAsync(ViewAccount ViewAccount);
        Task<string> LoginAsync(ViewAccount ViewAccount);
        Task<string> LoginUser(ViewAccount ViewAccount);
        Task<string> LoginAdmin(ViewAccount ViewAccount);
    }

}

