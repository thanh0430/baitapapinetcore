﻿using baitapapinetcore.Models;
using baitapapinetcore.Services.ProductService;
using baitapapinetcore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace baitapapinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var resuilt = await _productRepository.GetAllAsync();
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
                var reesuilt = await _productRepository.GetByIdAsync(id);
                return Ok(reesuilt);
            }
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ViewProducts viewProducts)
        {
            try 
            {
                var resuilt = await _productRepository.AddAsync(viewProducts);
                return Ok(resuilt);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
    
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ViewProducts viewProducts)
        {
            try 
            {          
                
                await _productRepository.UpdateAsync(viewProducts);
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task< IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
