using CommanApi.Interface;
using CommanApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommanApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _CategoryMasterservice;

        public CategoryController(ICategoryService CategoryMasterservice)
        {
            _CategoryMasterservice = CategoryMasterservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var data = await _CategoryMasterservice.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var data = await _CategoryMasterservice.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditCategory(CategoryMaster_IUD categoryMaster)
        {
            var data = await _CategoryMasterservice.CreateUpdateAsync(categoryMaster, categoryMaster.CM_CategoryID > 0 ? 1 : 0);
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var data = await _CategoryMasterservice.DeleteAsync(id);
            return Ok(data);
        }

    }
}
