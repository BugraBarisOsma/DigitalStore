namespace DigitalStore.Business.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Core.DTOs;

public interface ICategoryService
{
    Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO categoryDto);
    Task<CategoryResponseDTO> UpdateCategoryAsync(Guid id, CategoryRequestDTO categoryDto);
    Task<bool> DeleteCategoryAsync(Guid id);
    Task<List<CategoryResponseDTO>> GetCategoriesAsync();
    Task<List<ProductResponseDTO>> GetProductsByCategoryAsync(Guid categoryId);
}