using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseRepository<Product> _productrepository;

        public ProductsController(IBaseRepository<Product> productrepository)
        {
            _productrepository = productrepository;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productrepository.GetAll();
                if (products is null)
                    return NotFound();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet(" GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productrepository.GetById(id);
                if (product is null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProductDto product)
        {
            var newproduct = new Product 
            {  Name = product.Name ,
               List_Price=product.List_Price,
               Model_year=product.Model_year,
               CategoryId=product.CategoryId
            };
            await _productrepository.Add(newproduct);
            return Ok(newproduct);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDto product)
        {

            try
            {
                var newproduct = await _productrepository.GetById(id);
                if (product is null)
                    return NotFound();
                newproduct.Name = product.Name;
                await _productrepository.Update(newproduct);
                return Ok(newproduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var product = await _productrepository.GetById(id);
                if (product is null)
                    return NotFound();
                await _productrepository.Delete(product);
                    return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
