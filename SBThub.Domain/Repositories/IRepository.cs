using System.Linq.Expressions;
using SBThub.Domain.Common;

namespace SBThub.Domain.Repositories;

public interface IRepository
{
    Task<TEntity?> GetByUuidAsync<TEntity>(
        Guid uuid,
        CancellationToken ct,
        params Expression<Func<TEntity, object?>>[] includes)
        where TEntity : Entity;

    Task<TEntity?> GetSingleAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<TEntity, object?>>[] includes)
        where TEntity : class;

    Task Add<TEntity>(TEntity entity) where TEntity : Entity;

    Task Add<TEntity>(TEntity entity, CancellationToken ct) where TEntity : Entity;

    void Delete<TEntity>(TEntity entity) where TEntity : Entity;

    void Update<TEntity>(TEntity entity) where TEntity : Entity;

    Task<bool> AnyAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
        where TEntity : class;

    Task<bool> AnyIgnoringFiltersAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
        where TEntity : class;
}