using System.Text;
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

    public async Task<ProductViewModel> CreateProduct(ProductViewModel product)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        StringContent content = new StringContent(JsonSerializer
                                .Serialize(product), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _serializerOptions);
            }
            else
            {
                productVM = null;
            }
        }
        return productVM;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            return response.IsSuccessStatusCode;
        }
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productsVM = await JsonSerializer
                            .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _serializerOptions);
                return productsVM;
            }
            else
            {
                productsVM = null;
            }
        }
        return productsVM;
    }

    public async Task<ProductViewModel> GetProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _serializerOptions);
            }
            else
            {
                productVM = null;
            }
        }
        return productVM;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel product)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdated = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, product))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productUpdated = await JsonSerializer
                            .DeserializeAsync<ProductViewModel>(apiResponse, _serializerOptions);
            }
            else
            {
                productUpdated = null;
            }
        }
        return productUpdated;
    }
}
