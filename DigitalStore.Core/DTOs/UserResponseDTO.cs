namespace DigitalStore.Core.DTOs;

public class UserResponseDTO
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; } 
    public decimal WalletBalance { get; set; }
    public int Points { get; set; }
}