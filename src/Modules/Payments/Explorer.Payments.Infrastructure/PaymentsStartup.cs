using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Core.Mappers;
using Explorer.Payments.Core.UseCases.Shopping;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure;

public static class PaymentsStartup
{
    public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IPurchaseTokenService, PurchaseTokenService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<PurchaseToken>), typeof(CrudDatabaseRepository<PurchaseToken, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));

        services.AddScoped<IShoppingCartRepository, ShoppingCartDatabaseRepository>();
        services.AddScoped<IPurchaseTokenRepository, PurchaseTokenDatabaseRepository>();

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}
