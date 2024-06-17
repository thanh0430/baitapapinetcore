using baitapapinetcore.Services.OrderSevice;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitapapinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try 
            { 
                var resuilt = await _orderRepository.GetAllAsync();
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
                var resuilt = await _orderRepository.GetByIdAsync(id);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(List<CreateOrderRequest> createOrderRequests)
        {
            try
            {
                
                var result = await _orderRepository.AddAsync(createOrderRequests);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id,  ViewOrder ViewOrder)
        {
            try
            {
                await _orderRepository.UpdateAsync(ViewOrder, id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message) ;
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderRepository.DeleteAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}/GetOrderWithOrderDetail")]
        public async Task<IActionResult> GetOrderWithOrderDetail(int id)
        {
            var resuilt = await _orderRepository.GetOrderWithOrderDetail(id);
            return Ok(resuilt);
        }
        [HttpGet("/GetOrderby")]
        public async Task<IActionResult> GetOrderby()
        {
            try
            {
                var resuilt = await _orderRepository.GetOrderby();
                return Ok(resuilt);
            }
           catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetOrderbystatus")]
        public async Task<IActionResult> GetOrderbystatus()
        {
      
            try
            {
                var resuilt = await _orderRepository.GetOrderbystatus();
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetOrderchart")]
        public async Task<IActionResult> GetOrderchart()
        {
        
            try
            {
                var resuilt = await _orderRepository.GetOrderchart();
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetGellinPproducts")]
        public async Task<IActionResult> GetGellinPproducts()
        {
         
            try
            {
                var resuilt = await _orderRepository.GetGellinPproducts();
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetLatestProduct")]
        public async Task<IActionResult> GetLatestProduct()
        {
       
            try
            {
                var resuilt = await _orderRepository.GetLatestProduct();
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }    
}
