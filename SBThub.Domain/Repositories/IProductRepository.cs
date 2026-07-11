using SBThub.Domain.Entities;

namespace SBThub.Domain.Repositories;

public interface IProductRepository
{
    void Add(Product product);
    
    void Remove(Product product);

    Task<Product?> GetByUuidAsync(Guid uuid, CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);
    
    
    Task<IReadOnlyDictionary<int, Product>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken);
}