using DigitalStore.Core.DTOs;

namespace DigitalStore.Business.Services.Abstract;

public interface ICheckoutService
{
     Task<CheckoutResultDTO> CheckoutAsync(Guid orderId, string couponCode, decimal pointsToUse);
}