using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    /// <summary>
    /// Add a category
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory(CategoryRequestDTO categoryDto)
    {
        await _categoryService.AddCategoryAsync(categoryDto);
        return Ok();
    }
    /// <summary>
    /// Update a category
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequestDTO categoryDto)
    {
        await _categoryService.UpdateCategoryAsync(id, categoryDto);
        return Ok();
    }
    /// <summary>
    /// Get all categories
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        return Ok(categories);
    }
    /// <summary>
    /// Get all products by categories
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet("{id}/products")]
    public async Task<IActionResult> GetProductsByCategory(Guid id)
    {
        var products = await _categoryService.GetProductsByCategoryAsync(id);
        return Ok(products);
    }
    /// <summary>
    /// delete a category
    /// </summary>
    /// <response code="200">Success</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var result = await _categoryService.DeleteCategoryAsync(id);
        if (result)
            return Ok();
        else
            return BadRequest("Category cannot be deleted because it has associated products.");
    }
}