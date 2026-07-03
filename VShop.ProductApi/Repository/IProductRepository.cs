using VShop.ProductApi.Models;

namespace VShop.ProductApi.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(int id);
    Task<Product> Create(Product Product);
    Task<Product> Update(Product Product);

    //I can use Task<bool> for return true/false, I can return the deleted Product, or I can return the deleted Product's id. I will return the deleted Product.
    Task<Product> Delete(int id);
}
