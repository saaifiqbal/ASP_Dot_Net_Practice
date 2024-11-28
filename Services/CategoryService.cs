using asp_net_ecommerce_web_api.DTO;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.Models;
using AutoMapper;

namespace asp_net_ecommerce_web_api.Service
{
    public class CategoryService : ICategoryService
    {
        private static readonly List<Category> categories = new List<Category>();

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<ReadCategoryDto> GetAllCategories()
        {
            return _mapper.Map<List<ReadCategoryDto>>(categories);
            // return categories.Select(c => new ReadCategoryDto
            // {
            //     CategoryId = c.CategoryId,
            //     CategoryName = c.CategoryName,
            //     CategoryDescription = c.CategoryDescription,
            //     CreatedAt = c.CreatedAt
            // }).ToList();
        }

        public ReadCategoryDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = categoryData.CategoryName,
                CategoryDescription = categoryData.CategoryDescription,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);

            return _mapper.Map<ReadCategoryDto>(newCategory);
        }

        public List<ReadCategoryDto>? GetCategoryBySearch(string? searchValue)
        {
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
                    return _mapper.Map<List<ReadCategoryDto>>(foundCategories);
                };
                return null;
            }
            return _mapper.Map<List<ReadCategoryDto>>(categories);
        }
    }
}