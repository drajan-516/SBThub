using MediatR;
using Microsoft.AspNetCore.Mvc;
using SBThub.Domain.Shared;
using System.Linq.Expressions;
using SBThub.Domain.Common;
using SBThub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SBThub.Infrastructure.Persistence.Repositories;

internal sealed class BaseRepository(ShopDbContext context) : IRepository
{
    private IRepository _repositoryImplementation;
    private ShopDbContext Context => context;

    public async Task<TEntity?> GetByUuidAsync<TEntity>(
        Guid uuid,
        CancellationToken ct,
        params Expression<Func<TEntity, object?>>[] includes)
        where TEntity : Entity
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        query = includes.Aggregate(query, (current, include)
            => current.Include(include));

        return await query.FirstOrDefaultAsync(e => e.Uuid == uuid, ct);
    }

    public async Task<TEntity?> GetSingleAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default,
        params Expression<Func<TEntity, object?>>[] includes)
        where TEntity : class
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

        query = includes.Aggregate(query, (current, include)
            => current.Include(include));

        return await query.FirstOrDefaultAsync(predicate, ct);
    }

    public async Task Add<TEntity>(TEntity entity) where TEntity : Entity
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Add<TEntity>(TEntity entity, CancellationToken ct) where TEntity : Entity
    {
        await Context.Set<TEntity>().AddAsync(entity, ct);
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : Entity
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : Entity
    {
        Context.Set<TEntity>().Update(entity);
    }

    public async Task<bool> AnyAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
        where TEntity : class
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(predicate, ct);
    }

    public async Task<bool> AnyIgnoringFiltersAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default)
        where TEntity : class
    {
        return await Context.Set<TEntity>()
            .IgnoreQueryFilters()
            .AsNoTracking()
            .AnyAsync(predicate, ct);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(CancellationToken ct) where TEntity : class
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(ct);
    }
}