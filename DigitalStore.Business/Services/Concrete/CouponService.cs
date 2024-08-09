using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using AutoMapper;
using DigitalStore.Core.Domains;
using DigitalStore.Core.DTOs;
using DigitalStore.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalStore.Business.Services.Concrete;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CouponResponseDTO> CreateCouponAsync(CouponRequestDTO couponDto)
    {
        var coupon = _mapper.Map<Coupon>(couponDto);
        await _unitOfWork.GetRepository<Coupon>().AddAsync(coupon);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CouponResponseDTO>(coupon);
    }
    
    
    public async Task<List<CouponResponseDTO>> GetCouponsAsync()
    {
        var coupons = await _unitOfWork.GetRepository<Coupon>().GetAllAsync();
        return _mapper.Map<List<CouponResponseDTO>>(coupons);
    }

    public async Task DeleteCouponAsync(Guid id)
    {
        var coupon = await _unitOfWork.GetRepository<Coupon>().GetByIdAsync(id);
        if (coupon == null)
        {
            throw new Exception("Coupon not found");
        }

        await _unitOfWork.GetRepository<Coupon>().DeleteAsync(coupon);
        await _unitOfWork.SaveChangesAsync();
    }
}
