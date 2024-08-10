using AutoMapper;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Core.Domains;
using DigitalStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Business.Services.Concrete;


public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task AddProductAsync(ProductRequestDTO productDto)
    {
        
        var product = _mapper.Map<Product>(productDto);
        var productRepository = _unitOfWork.GetRepository<Product>();
        await productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

      
        var productCategoryRepository = _unitOfWork.GetRepository<ProductCategory>();
        foreach (var categoryId in productDto.CategoryIds)
        {
            var productCategory = new ProductCategory
            {
                Id= Guid.NewGuid(),
                ProductId = product.Id,
                CategoryId = categoryId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
                
            };

            await productCategoryRepository.AddAsync(productCategory);
        }
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task UpdateProductAsync(Guid id, ProductRequestDTO productDto)
    {
        var productRepository = _unitOfWork.GetRepository<Product>();
        var existingProduct = await productRepository.GetByFilterAsync(p => p.Id == id);
        if (existingProduct == null)
        {
            throw new KeyNotFoundException("Product not found");
        }
        _mapper.Map(productDto, existingProduct);
        await productRepository.UpdateAsync(existingProduct);
        await _unitOfWork.SaveChangesAsync();
        var productCategoryRepository = _unitOfWork.GetRepository<ProductCategory>();

       
        var existingProductCategories = await productCategoryRepository.GetAllByFilterAsync(pc => pc.ProductId == id);
        foreach (var existingCategory in existingProductCategories)
        {
            await productCategoryRepository.DeleteAsync(existingCategory);
        }

        
        foreach (var categoryId in productDto.CategoryIds)
        {
            var productCategory = new ProductCategory
            {
                ProductId = id,
                CategoryId = categoryId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            };

            await productCategoryRepository.AddAsync(productCategory);
        }
        await _unitOfWork.SaveChangesAsync();
    }


    public async Task<List<ProductResponseDTO>> GetProductsAsync()
    {
      
        var products = await _unitOfWork.GetRepository<Product>().GetAllByFilterAsync(x=>x.IsActive,query => query.Include(x => x.ProductCategories).ThenInclude(pc=>pc.Category));
        return _mapper.Map<List<ProductResponseDTO>>(products);
    }
    public async Task<List<ProductResponseDTO>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var productCategoryRepository = _unitOfWork.GetRepository<ProductCategory>();
        var productRepository = _unitOfWork.GetRepository<Product>();
        
        var productCategories = await productCategoryRepository.GetAllByFilterAsync(pc => pc.CategoryId == categoryId);
        var productIds = productCategories.Select(pc => pc.ProductId).Distinct();
        var products = await productRepository.GetAllByFilterAsync(p => productIds.Contains(p.Id) && p.IsActive,include: p => p.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category));

        var productsResponse = _mapper.Map<List<ProductResponseDTO>>(products);
        return productsResponse;
    }


    public async Task DeleteProductAsync(Guid id)
    {
        var productRepository = _unitOfWork.GetRepository<Product>();
        var product = await productRepository.GetByFilterAsync(p => p.Id == id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }
        product.IsActive = false;
        await productRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }
}