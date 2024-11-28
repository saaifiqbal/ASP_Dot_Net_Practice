using asp_net_ecommerce_web_api.DTO;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.Models;
using asp_net_ecommerce_web_api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();
        private ICategoryService _categoryService;

        public CategoryController () {
            _categoryService = new CategoryService();
        }

        //GET: api/categories => Read Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var newCategory = _categoryService.GetAllCategories();
            return Ok(ApiResponse<List<ReadCategoryDto>>.SuccessResponse(newCategory, "Category Fetch Successfully"));
        }

        //GET: api/category?SearchValue="" => Find and Retrieve Data
        [HttpGet("search")]
        public IActionResult GetCategory([FromQuery] string? searchValue = "")
        {
            // Check if a search value was provided
            var SearchCategories = _categoryService.GetCategoryBySearch(searchValue);

            if(SearchCategories == null ){
                return Ok(ApiResponse<List<ReadCategoryDto?>>.ErrorResponse("Not Found Any Data"));
            }
            return Ok(ApiResponse<List<ReadCategoryDto>>.SuccessResponse(SearchCategories, "Category Fetch Successfully"));
        }


        //POST: api/category => Create Category
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = _categoryService.CreateCategory(categoryData);
            return Created($"/api/categories/{newCategory.CategoryId}", ApiResponse<ReadCategoryDto>.SuccessResponse(newCategory, "Category Create Successfully"));
        }

        //PUT: api/category/{categoryId} => Update Category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, UpdateCreateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null) return NotFound(new { Message = "No Category Found", isSuccess = false });
            foundCategory.CategoryName = categoryData.CategoryName;
            foundCategory.CategoryDescription = categoryData.CategoryDescription;
            return Ok(ApiResponse<string>.SuccessResponse("1", "Category Updated Successfully"));
        }

        //DELETE: api/category/{categoryId} => Delete Category
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (foundCategory == null) return NotFound(new { Message = "No Category Found", isSuccess = false });
            categories.Remove(foundCategory);
            return Ok(ApiResponse<string>.SuccessResponse("1", "Category Delete Successfully"));
        }
    }

}