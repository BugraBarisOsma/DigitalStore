using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    /// <summary>
    /// Add a new product
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Unauthorized</response>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProduct(ProductRequestDTO productDto)
    {
        await _productService.AddProductAsync(productDto);
        return Ok();
    }
    /// <summary>
    /// Update a product
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Unauthorized</response>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductRequestDTO productDto)
    {
        await _productService.UpdateProductAsync(id, productDto);
        return Ok();
    }
    /// <summary>
    /// Get all products
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
    /// <summary>
    /// Get all products by category
    /// </summary>
    /// <response code="200">Success</response>
    [HttpGet("by-category")]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] Guid categoryId)
    {
        if (categoryId == Guid.Empty)
        {
            return BadRequest("Invalid category ID.");
        }

        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }
    /// <summary>
    /// Delete a product
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await _productService.DeleteProductAsync(id);
        return Ok();
    }
}