using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Helpers;

public static class AuthGuard
{
    public static bool RequireAuth(AuthService authService, string token)
    {
        var validToken = authService.ValidateToken(token);
        if (validToken is null)
        {
            Console.WriteLine("Unauthorized: invalid or expired token.");
            return false;
        }
        return true;
    }

    public static int? GetUserId(AuthService authService, string token)
    {
        return authService.ValidateToken(token)?.UserId;
    }
}
