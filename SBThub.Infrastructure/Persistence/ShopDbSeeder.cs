using SBThub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SBThub.Infrastructure.Persistence;

public static class ShopDbSeeder
{
    public static async Task SeedAsync(ShopDbContext context, CancellationToken cancellationToken = default)
    {
        
        if (await context.Users.AnyAsync(cancellationToken))
            return;

        var user = User.Create("Домбровская Аня", "+380 555 35 35").Value;
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}
