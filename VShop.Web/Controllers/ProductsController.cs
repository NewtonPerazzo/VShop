using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VShop.Web.Models;
using VShop.Web.Services.Contracts;

namespace VShop.Web.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts();

        if (result is null)
            return View("Error");

        //return view with the list of products
        return View(result);
    }

    [HttpGet]
    public async Task<ActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(product);
            if (result != null)
                return RedirectToAction(nameof(Index));

        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View(product);
    }

    [HttpGet]
    public async Task<ActionResult> UpdateProduct(int id)
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        var result = await _productService.GetProductById(id);
        if (result is null)
            return View("Error");
        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult> UpdateProduct(ProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(product);
            if (result != null)
                return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(), "CategoryId", "Name");
        return View(product);
    }

    [HttpGet]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var result = await _productService.GetProductById(id);
        if (result is null)
            return View("Error");
        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")] //actionname does not match the method name, so we need to specify it here and use the same name in the view form action instead of deleteconfirmed
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        var result = await _productService.DeleteProduct(id);
        if (result == null)
            return View("Error");

         return RedirectToAction("Index");
    }
}
