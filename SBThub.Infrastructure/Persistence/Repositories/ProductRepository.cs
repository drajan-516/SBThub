using SBThub.Domain.Entities;
using SBThub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SBThub.Infrastructure.Persistence.Repositories;

internal sealed class ProductRepository(ShopDbContext context) : IProductRepository
{
    public void Add(Product product) => context.Products.Add(product);

    public void Remove(Product product) => context.Products.Remove(product);

    public async Task<Product?> GetByUuidAsync(Guid uuid, CancellationToken cancellationToken) =>
        await context.Products.FirstOrDefaultAsync(product => product.Uuid == uuid, cancellationToken);

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken) =>
        await context.Products
            .AsNoTracking()
            .OrderBy(product => product.Id)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyDictionary<int, Product>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
            return new Dictionary<int, Product>();

        return await context.Products
            .AsNoTracking()
            .Where(product => ids.Contains(product.Id))
            .ToDictionaryAsync(product => product.Id, cancellationToken);
    }
}