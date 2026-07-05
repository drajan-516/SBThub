using SBThub.Domain.Entities;

namespace SBThub.Domain.Repositories;

public interface IUserRepository
{
    void Add(User user);
    
    void Remove(User user);

    Task<User?> GetByUuidAsync(Guid uuid, CancellationToken cancellationToken);

    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<IReadOnlyDictionary<int, User>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken);
}
