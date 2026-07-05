using System.Text.Json;
using VShop.Web.Models;

namespace VShop.Web.Services.Contracts;

public class ProductService : IProductService
{
    public readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _serializerOptions;
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public Task<ProductViewModel> CreateProduct(ProductViewModel product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> UpdateProduct(ProductViewModel product)
    {
        throw new NotImplementedException();
    }
}
