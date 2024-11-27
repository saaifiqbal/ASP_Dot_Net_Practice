using asp_net_ecommerce_web_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/controller/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        //GET: api/categories => Read Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(new { Message = "Successfully Get", isSuccess = true, Results = categories });
        }

        //GET: api/category?SearchValue="" => Find and Retrieve Data
        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategory(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {
                var foundCategory = categories.Where(c => c.CategoryName == searchValue);
                if (foundCategory != null)
                {
                    return Ok(new { Message = "Successfully Get", isSuccess = true, Results = foundCategory });
                }
            }
            return Ok(new { Message = "Successfully Get", isSuccess = true, Results = categories });

        }

        //POST: api/category => Create Category
        [HttpPost]
        public IActionResult CreateCategory(Category categoryData)
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
        public IActionResult UpdateCategory(Guid categoryId, Category categoryData)
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