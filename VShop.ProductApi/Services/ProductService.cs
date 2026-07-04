using AutoMapper;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Models;
using VShop.ProductApi.Repository;

namespace VShop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task AddProduct(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Create(productEntity);
        productDTO.Id = productEntity.Id;
    }

    public async Task<ProductDTO> GetProductById(int id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(productEntity);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task RemoveProduct(int id)
    {
        var productEntity = _productRepository.GetById(id).Result;
        await _productRepository.Delete(productEntity.Id);
    }

    public async Task UpdateProduct(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Update(productEntity);
    }
}
