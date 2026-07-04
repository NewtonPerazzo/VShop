using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VShop.ProductApi.DTOs;
using VShop.ProductApi.Services;

namespace VShop.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categoriesDTO = await _categoryService.GetCategories();
        if(categoriesDTO is null)
            return NotFound("Categories not found");
        return Ok(categoriesDTO);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetById(int id)
    {
        var categoryDTO = await _categoryService.GetCategoryById(id);
        if (categoryDTO is null)
            return NotFound("Category not found");
        return Ok(categoryDTO);
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryProducts()
    {
        var categoriesDTO = await _categoryService.GetCategoryProducts();
        if (categoriesDTO is null)
            return NotFound("Categories not found");
        return Ok(categoriesDTO);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categoryDTO)
    {
        if (categoryDTO is null)
            return BadRequest("Invalid data");
        await _categoryService.AddCategory(categoryDTO);
        // return category getting by id
        return new CreatedAtActionResult("GetCategory", "Category", new { id = categoryDTO.CategoryId }, categoryDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Put(int id, [FromBody] CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.CategoryId)
            return BadRequest("Invalid data");

        if (categoryDTO == null)
            return BadRequest();
        await _categoryService.UpdateCategory(categoryDTO);
        return Ok(categoryDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<CategoryDTO>> Delete(int id)
    {
        var categoryDTO = await _categoryService.GetCategoryById(id);
        if (categoryDTO is null)
            return NotFound("Category not found");
        await _categoryService.RemoveCategory(id);
        return Ok(categoryDTO);
    }
}
