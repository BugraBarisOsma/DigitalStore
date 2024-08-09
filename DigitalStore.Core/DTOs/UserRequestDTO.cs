namespace DigitalStore.Core.DTOs;

public class UserRequestDTO
{
    public string Username { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    //public string Role { get; set; } // Normal, Admin
    //public bool IsActive { get; set; }
    //public decimal WalletBalance { get; set; }
    //public int Points { get; set; }
}