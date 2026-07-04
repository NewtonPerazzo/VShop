using VShop.ProductApi.DTOs;

namespace VShop.ProductApi.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategories();
    Task<IEnumerable<CategoryDTO>> GetCategoryProducts();
    Task<CategoryDTO> GetCategoryById(int id);
    Task AddCategory(CategoryDTO categoryDTO);
    Task RemoveCategory(int id);
    Task UpdateCategory(CategoryDTO categoryDTO);
}
