using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var productsDTO = await _productService.GetProducts();
        if (productsDTO is null)
            return NotFound("Products not found");
        return Ok(productsDTO);
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var productDTO = await _productService.GetProductById(id);
        if (productDTO is null)
            return NotFound("Product not found");
        return Ok(productDTO);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO productDTO)
    {
        if (productDTO is null)
            return BadRequest("Invalid data");
        await _productService.AddProduct(productDTO);

        // return product getting by id using the named route to avoid controller/action name mismatches
        return CreatedAtRoute("GetProduct", new { id = productDTO.Id }, productDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody] ProductDTO productDTO)
    {
        if (id != productDTO.Id)
            return BadRequest("Invalid data");

        if (productDTO == null)
            return BadRequest();
        await _productService.UpdateProduct(productDTO);
        return Ok(productDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProductDTO>> Delete(int id)
    {
        var productDTO = await _productService.GetProductById(id);
        if (productDTO is null)
            return NotFound("Product not found");
        await _productService.RemoveProduct(id);
        return Ok(productDTO);
    }
}
