using AutoMapper;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DigitalStore.Business.Services.Concrete
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CheckoutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CheckoutResultDTO> CheckoutAsync(Guid orderId, string couponCode, decimal pointsToUse)
        {
            var order = await _unitOfWork.GetRepository<Order>()
                .GetByFilterAsync(o => o.Id == orderId && o.IsActive, include: q => q.Include(o => o.OrderDetails).Include(o => o.User));
            
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            decimal totalAmount = order.TotalAmount;
            var user = order.User;

            if (!string.IsNullOrEmpty(couponCode))
            {
                var coupon = await _unitOfWork.GetRepository<Coupon>()
                    .GetByFilterAsync(x => x.Code == couponCode && x.IsActive == false && x.ExpiryDate >= DateTime.UtcNow);
                
                if (coupon == null)
                {
                    throw new Exception("Coupon not found");
                }

                totalAmount -= coupon.Amount;
                totalAmount = Math.Max(totalAmount, 0);

                coupon.IsActive = true;
                await _unitOfWork.GetRepository<Coupon>().UpdateAsync(coupon);
            }

            var totalPointsEarned = await CalculateRewardPoints(order);

            pointsToUse = Math.Min(pointsToUse, totalPointsEarned);
            totalAmount -= pointsToUse;

            if (totalAmount < 0)
            {
                totalAmount = 0;
            }

            if (totalAmount > user.WalletBalance)
            {
                throw new Exception("Insufficient wallet balance");
            }

            user.WalletBalance -= totalAmount;
            user.Points += (int)Math.Floor(totalPointsEarned - pointsToUse);
            
            order.TotalAmount = totalAmount;
            order.PointsUsed = pointsToUse;
            order.CouponCode = couponCode;

            await _unitOfWork.GetRepository<Order>().UpdateAsync(order);
            await _unitOfWork.GetRepository<User>().UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new CheckoutResultDTO()
            {
                UserId = user.Id,
                TotalAmount = totalAmount,
                PointsEarned = totalPointsEarned - pointsToUse
            };
        }

        private async Task<decimal> CalculateRewardPoints(Order order)
        {
            decimal totalPoints = 0;

            foreach (var detail in order.OrderDetails)
            {
                var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(detail.ProductId);
                if (product != null)
                {
                    var points = detail.Price * (decimal)product.RewardPointsPercentage / 100;
                    points = Math.Min(points, product.MaxRewardPoints);
                    totalPoints += points;
                }
            }

            return totalPoints;
        }
    }
}
