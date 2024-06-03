using baitapapinetcore.Services.CatrgorySevice;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitapapinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var resuilt = await _categoryRepository.GetAllAsync();
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
                var resuilt = _categoryRepository.GetByIdAsync(id);
                return Ok(resuilt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpGet("/GetCategoryWithProducts")]
        public async Task<IActionResult> GetCategoryWithProducts()
        {
            try
            {
                var result = await _categoryRepository.GetCategoryWithProducts();

                if (result == null)
                {
                    return NotFound(); 
                }

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetCategoryWithProductsExplicitly")]
        public async Task<IActionResult> GetCategoryWithProductsExplicitly(int id)
        {
            try
            {
                var result = await _categoryRepository.GetCategoryWithProductsExplicitly(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/GetCategoryWithProductsLazyloading")]
        public async Task<IActionResult> GetCategoryWithProductsLazyloading(int id)
        {
            try
            {
                var result = await _categoryRepository.GetCategoryWithProductsLazyloading(id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] ViewCategory ViewCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _categoryRepository.AddAsync(ViewCategory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(ViewCategory viewCategory)
        {
            try
            {
                await _categoryRepository.UpdateAsync(viewCategory);
                return Ok();
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
