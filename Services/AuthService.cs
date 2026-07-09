using TaskManager.Models;

namespace TaskManager.Services;

public class AuthService
{
    private readonly UserService _userService;
    private readonly List<AuthToken> _tokens = new();

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public AuthToken? Login(string email, string password)
    {
        var user = _userService.FindByEmail(email);
        if (user is null) return null;

        // TODO: Replace with proper password hashing
        var token = new AuthToken
        {
            Token = Guid.NewGuid().ToString("N"),
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddHours(8)
        };
        _tokens.Add(token);
        return token;
    }

    public AuthToken? ValidateToken(string token)
    {
        var authToken = _tokens.FirstOrDefault(t => t.Token == token);
        if (authToken is null || authToken.IsExpired) return null;
        return authToken;
    }

    public void Logout(string token)
    {
        var authToken = _tokens.FirstOrDefault(t => t.Token == token);
        if (authToken is not null)
            _tokens.Remove(authToken);
    }
}
