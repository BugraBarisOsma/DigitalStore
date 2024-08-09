using AutoMapper;
using DigitalStore.Business.Services.Abstract;
using DigitalStore.Core.DTOs;
using DigitalStore.Core.Domains;
using DigitalStore.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using DigitalStore.Business.Helpers;

namespace DigitalStore.Business.Services.Concrete;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtGenerator _jwtTokenGenerator;

    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        JwtGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<User> RegisterUserAsync(UserRequestDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user.Role = "Customer";
        user.isActive = true;
        user.WalletBalance = 1000;
        user.Points = 10;
        user.isActive = true;
       
        var regiesteredUser  = await _unitOfWork.GetRepository<User>().AddAsync(user);
        return regiesteredUser;

    }

    public async Task<string?> LoginUserAsync(UserRequestDTO userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, false, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            return _jwtTokenGenerator.GenerateToken(user);
        }

        return null;
    }

    public async Task UpdateUserAsync(Guid id, UserRequestDTO  userDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new KeyNotFoundException("User not found");

        _mapper.Map(userDto, user);
        await _userManager.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new KeyNotFoundException("User not found");

        await _userManager.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<decimal> GetUserPointsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("User not found");

        // Assuming points are stored in the user entity
        return user.Points;
    }
}
