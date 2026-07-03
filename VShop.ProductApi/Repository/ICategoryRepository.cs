using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAll();
    Task<IEnumerable<Category>> GetCategoriesProducts();
    Task<Category> GetById(int id);
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);

    //I can use Task<bool> for return true/false, I can return the deleted category, or I can return the deleted category's id. I will return the deleted category.
    Task<Category> Delete(int id);
}
