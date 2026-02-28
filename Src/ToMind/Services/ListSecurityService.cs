using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using ToMind.Data;

namespace ToMind.Services;

public sealed class ListSecurityService
{
    private readonly PasswordHasher<MindList> _hasher = new();

    public string HashPassword(MindList list, string password)
    {
        return _hasher.HashPassword(list, password);
    }

    public bool VerifyPassword(MindList list, string? passwordHash, string password)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return false;
        }

        var result = _hasher.VerifyHashedPassword(list, passwordHash, password);
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    public (string Token, string TokenHash) CreateRememberMeToken(MindList list)
    {
        var token = GenerateToken();
        var tokenHash = _hasher.HashPassword(list, token);
        return (token, tokenHash);
    }

    public bool VerifyRememberMeToken(MindList list, string? tokenHash, string token)
    {
        if (string.IsNullOrWhiteSpace(tokenHash))
        {
            return false;
        }

        var result = _hasher.VerifyHashedPassword(list, tokenHash, token);
        return result == PasswordVerificationResult.Success
            || result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    private static string GenerateToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return WebEncoders.Base64UrlEncode(bytes);
    }
}
