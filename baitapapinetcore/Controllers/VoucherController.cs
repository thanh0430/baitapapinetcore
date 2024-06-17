using baitapapinetcore.Services.VoucherService;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitapapinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository) 
        {
            _voucherRepository = voucherRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var resuilt = await _voucherRepository.GetAll();
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
                var resuilt =await _voucherRepository.GetById(id);
                return Ok(resuilt);
            }
            catch(Exception ex) 
            {
            return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateVoucher(ViewVoucher viewVoucher)
        {
            try
            {
                var resuilt = await _voucherRepository.CreateVoucher(viewVoucher);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVoucher(ViewVoucher viewVoucher, int id)
        {
            try
            {
                await _voucherRepository.UpdateVoucher(viewVoucher, id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id)
        {
            try
            {
                await _voucherRepository.DeleteVoucher(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
