namespace DigitalStore.Business.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalStore.Core.DTOs;

public interface ICouponService
{
    Task<CouponResponseDTO> CreateCouponAsync(CouponRequestDTO couponDto);
    Task<List<CouponResponseDTO>> GetCouponsAsync();
    Task DeleteCouponAsync(Guid id);
}
