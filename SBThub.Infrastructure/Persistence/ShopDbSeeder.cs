using System.Runtime.InteropServices.JavaScript;
using SBThub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SBThub.Infrastructure.Persistence;

public static class ShopDbSeeder
{
    public static async Task SeedAsync(ShopDbContext context, CancellationToken cancellationToken = default)
    {
        
        if (await context.Users.AnyAsync(cancellationToken))
            return;

        var user = User.Create("Домбровская Аня", "+3805553535").Value;
        context.Users.Add(user);
        await context.SaveChangesAsync(cancellationToken);
        
        
        if (await context.Products.AnyAsync(cancellationToken))
            return;

        var product = Product.Create("Asus Tuf Gaming", "Asus Tuf Gaming", 6738, DateTime.Now, user.Uuid).Value; 
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}

//add product
