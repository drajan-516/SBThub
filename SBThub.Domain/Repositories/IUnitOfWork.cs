namespace SBThub.Domain.Repositories;

/// <summary>
/// «Единица работы» (Unit of Work): сохраняет все накопленные изменения в базу данных
/// одним вызовом. Отделяет «что меняем» (репозиторий) от «когда фиксируем» (этот интерфейс).
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
