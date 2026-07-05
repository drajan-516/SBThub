using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SBThub.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(ShopDbContext context) : IUserRepository
{
    public void Add(User user) => context.Users.Add(user);

    public void Remove(User user) => context.Users.Remove(user);

    public async Task<User?> GetByUuidAsync(Guid uuid, CancellationToken cancellationToken) =>
        await context.Users.FirstOrDefaultAsync(user => user.Uuid == uuid, cancellationToken);

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Users
            .AsNoTracking()
            .OrderBy(user => user.Id)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyDictionary<int, User>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
            return new Dictionary<int, User>();

        return await context.Users
            .AsNoTracking()
            .Where(user => ids.Contains(user.Id))
            .ToDictionaryAsync(user => user.Id, cancellationToken);
    }
}
