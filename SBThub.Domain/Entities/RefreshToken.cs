using SBThub.Domain.Common;

namespace SBThub.Domain.Entities;

public sealed class RefreshToken : Entity
{
    private RefreshToken() { }

    public Guid UserUuid { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    
    public bool IsActive => !IsRevoked && ExpiresAt > DateTime.UtcNow;

    public static RefreshToken Create(Guid userUuid, string tokenHash, DateTime expiresAt) => new()
    {
        UserUuid = userUuid,
        TokenHash = tokenHash,
        ExpiresAt = expiresAt
    };

    public void Revoke() => IsRevoked = true;
}