using Microsoft.EntityFrameworkCore;
using Portfolio.Web.Data;
using Portfolio.Web.Helpers;
using Portfolio.Web.Models;

namespace Portfolio.Web.Services;

public interface IAuthService
{
    Task<(string Token, User? User)> LoginAsync(string email, string password);
    Task<User?> GetUserByIdAsync(int id);
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<(string Token, User? User)> LoginAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return (string.Empty, null);

        if (!PasswordHelper.VerifyPassword(password, user.Password)) return (string.Empty, null);

        var token = JwtHelper.GenerateToken(user.Id, _configuration);
        return (token, user);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
}
