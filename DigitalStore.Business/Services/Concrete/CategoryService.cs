using DigitalStore.Business.Services.Abstract;

namespace DigitalStore.Business.Services.Concrete;
using AutoMapper;
using DigitalStore.Core.Domains;
using DigitalStore.Core.DTOs;
using DigitalStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CategoryResponseDTO> AddCategoryAsync(CategoryRequestDTO categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _unitOfWork.GetRepository<Category>().AddAsync(category);
        await _unitOfWork.SaveChangesAsync(); 
        return _mapper.Map<CategoryResponseDTO>(category);
    }

    public async Task<CategoryResponseDTO> UpdateCategoryAsync(Guid id, CategoryRequestDTO categoryDto)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        _mapper.Map(categoryDto, category);
        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CategoryResponseDTO>(category);
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await _unitOfWork.GetRepository<Category>().GetByIdAsync(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

      
        var productCategoryRepo = _unitOfWork.GetRepository<ProductCategory>();
        var productsInCategory = await productCategoryRepo.GetAllByFilterAsync(pc => pc.CategoryId == id);

        if (productsInCategory.Any())
        {
            return false; // Category cannot be deleted
        }

        category.IsActive = false;
        await _unitOfWork.GetRepository<Category>().UpdateAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<CategoryResponseDTO>> GetCategoriesAsync()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync();
        return _mapper.Map<List<CategoryResponseDTO>>(categories);
    }

    public async Task<List<ProductResponseDTO>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var productCategories = await _unitOfWork.GetRepository<ProductCategory>().GetAllByFilterAsync(pc => pc.CategoryId == categoryId);
        var productIds = productCategories.Select(pc => pc.ProductId).Distinct();

        var products = await _unitOfWork.GetRepository<Product>().GetAllByFilterAsync(p => productIds.Contains(p.Id));
        return _mapper.Map<List<ProductResponseDTO>>(products);
    }
}
