
using DigitalStore.Core.DTOs;
namespace DigitalStore.Business.Services.Abstract;



public interface IProductService
{
    Task AddProductAsync(ProductRequestDTO productDto);
    Task UpdateProductAsync(Guid id, ProductRequestDTO productDto);
    Task<List<ProductResponseDTO>> GetProductsAsync();
    Task<List<ProductResponseDTO>> GetProductsByCategoryAsync(Guid categoryId);
    Task DeleteProductAsync(Guid id);
}