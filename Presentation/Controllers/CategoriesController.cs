using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dto;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBaseRepository<Category> _categoryrepository;

        public CategoriesController(IBaseRepository<Category> categoryrepository)
        {
            _categoryrepository = categoryrepository;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult>GetAll()
        {
            try
            {
                var categories = await _categoryrepository.GetAll();
                if (categories is null)
                    return NotFound();
                return Ok(categories);
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
                var category = await _categoryrepository.GetById(id);
                if (category is null)
                    return NotFound();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CategoryDto category)
        {
            var newcategory=new Category {  Name = category.Name };
           await _categoryrepository.Add(newcategory);   
            return Ok(newcategory);
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryDto category)
        {

            try
            {
                var newcategory = await _categoryrepository.GetById(id);
                if (category is null)
                    return NotFound();
                newcategory.Name = category.Name;
                await _categoryrepository.Update(newcategory);
                return Ok(newcategory);
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
                var category = await _categoryrepository.GetById(id);
                if (category is null)
                    return NotFound();
                await _categoryrepository.Delete(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
