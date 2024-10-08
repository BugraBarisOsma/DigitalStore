using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    
    //identity ile yasanan conflictten dolayi bu kısmı override ettim
    [JsonIgnore]
    public override string UserName { get; set; }
    
    public string Username { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "Customer";
    public bool isActive { get; set; } = true;
    public decimal WalletBalance { get; set; } = 2000;
    public List<Order> Orders { get; set; }
    public int Points { get; set; } = 10;
}