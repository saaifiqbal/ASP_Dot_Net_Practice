using asp_net_ecommerce_web_api.DTO;
using asp_net_ecommerce_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        //GET: api/categories => Read Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var newCategory = categories.Select(c=> new ReadCreateDto {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDescription,
                CreatedAt= c.CreatedAt
            });
            return Ok(new { Message = "Successfully Get", isSuccess = true, Results = newCategory });
        }

        //GET: api/category?SearchValue="" => Find and Retrieve Data
        [HttpGet("search")]
        public IActionResult GetCategory([FromQuery] string? searchValue = "")
        {
            // Check if a search value was provided
            if (!string.IsNullOrEmpty(searchValue))
            {
                // Search categories by name (case-insensitive, partial matches)
                var foundCategories = categories
                    .Where(c => c.CategoryName != null &&
                                c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Return found categories or a "not found" message
                if (foundCategories.Any())
                {
                    return Ok(new
                    {
                        Message = "Successfully retrieved matching categories.",
                        isSuccess = true,
                        Results = foundCategories
                    });
                }

                return NotFound(new
                {
                    Message = "No matching categories found.",
                    isSuccess = false,
                    Results = Array.Empty<Category>()
                });
            }

            // If no search value provided, return all categories
            return Ok(new
            {
                Message = "Successfully retrieved all categories.",
                isSuccess = true,
                Results = categories
            });
        }


        //POST: api/category => Create Category
        [HttpPost]
        public IActionResult CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = categoryData.CategoryName,
                CategoryDescription = categoryData.CategoryDescription,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);
            return Created($"/api/categories/{newCategory.CategoryId}", newCategory);
        }

        //PUT: api/category/{categoryId} => Update Category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, UpdateCreateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null) return NotFound(new { Message = "No Category Found", isSuccess = false });
            foundCategory.CategoryName = categoryData.CategoryName;
            foundCategory.CategoryDescription = categoryData.CategoryDescription;
            return NoContent();
        }

        //DELETE: api/category/{categoryId} => Delete Category
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (foundCategory == null) return NotFound(new { Message = "No Category Found", isSuccess = false });
            categories.Remove(foundCategory);
            return NoContent();
        }
    }

}