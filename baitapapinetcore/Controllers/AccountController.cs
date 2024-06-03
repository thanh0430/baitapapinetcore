using baitapapinetcore.Services.AccountSevice;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitapapinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository= accountRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try 
            { 
                var resuilt = await _accountRepository.GetAllAsync();
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var resuilt = await _accountRepository.GetByIdAsync(id);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAccount( ViewAccount viewAccount)
        {
            try
            {
                var resuilt = await _accountRepository.AddAccount(viewAccount);
                return Ok(resuilt);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(ViewAccount viewAccount)
        {
            try
            {
                await _accountRepository.UpdateAsync(viewAccount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                await _accountRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(ViewAccount viewAccount)
        {
            try
            {
                var resuilt = await _accountRepository.LoginAdmin(viewAccount);
                return Ok(resuilt);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
        [HttpPost("/LoginUser")]
        public async Task<IActionResult> LoginUser(ViewAccount viewAccount)
        {
            try
            {
                var ressuilt = await _accountRepository.LoginUser(viewAccount);
                return Ok(ressuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/RegisterUserAsync")]
        public async Task<IActionResult> RegisterUserAsync(ViewAccount viewAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var resuilt = await _accountRepository.RegisterUserAsync(viewAccount);
                return Ok(resuilt);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
