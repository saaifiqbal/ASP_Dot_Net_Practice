using asp_net_ecommerce_web_api.DTO;

namespace asp_net_ecommerce_web_api.Interface
{
    public interface ICategoryService
    {
        List<ReadCategoryDto> GetAllCategories();
        ReadCategoryDto CreateCategory(CategoryCreateDto categoryData);
        List<ReadCategoryDto>? GetCategoryBySearch(string? searchValue);
    }
};