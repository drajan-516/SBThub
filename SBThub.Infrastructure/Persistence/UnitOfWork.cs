using SBThub.Domain.Repositories;

namespace SBThub.Infrastructure.Persistence;

internal sealed class UnitOfWork(ShopDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        context.SaveChangesAsync(cancellationToken);
}
