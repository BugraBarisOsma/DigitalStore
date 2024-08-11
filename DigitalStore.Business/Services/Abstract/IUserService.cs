using System;
using System.Threading.Tasks;
using DigitalStore.Core.DTOs;
using Microsoft.AspNetCore.Identity;
namespace DigitalStore.Business.Services.Abstract;


public interface IUserService
{
    Task<UserResponseDTO> RegisterUserAsync(UserRequestDTO userDTO);
    Task UpdateUserAsync(Guid id, UserRequestDTO userDTO);
    Task DeleteUserAsync(Guid id);
    Task<decimal> GetUserPointsAsync(string userId); // This method assumes that points are handled separately
}