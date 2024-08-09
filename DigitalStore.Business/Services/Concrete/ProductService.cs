using AutoMapper;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Core.Domains;
using DigitalStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }

    public async Task<List<ProductResponseDTO>> GetProductsAsync()
    {
        var productRepository = _unitOfWork.GetRepository<Product>();
        var products = await productRepository.GetAllAsync();
        return _mapper.Map<List<ProductResponseDTO>>(products);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var productRepository = _unitOfWork.GetRepository<Product>();
        var product = await productRepository.GetByFilterAsync(p => p.Id == id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        await productRepository.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }
}