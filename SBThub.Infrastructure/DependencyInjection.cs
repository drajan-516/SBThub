using SBThub.Domain.Repositories;
using SBThub.Infrastructure.Persistence;
using SBThub.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SBThub.Application.Abstractions;
using SBThub.Infrastructure.Security;

namespace SBThub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ShopDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IRepository, BaseRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}


//add product