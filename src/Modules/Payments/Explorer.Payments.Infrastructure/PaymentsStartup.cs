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
using Explorer.Payments.API.Internal;
using Explorer.Payments.Core.UseCases;

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
        services.AddScoped<IPaymentRecordService, PaymentRecordService>();
        services.AddScoped<IPurchaseTokenService, PurchaseTokenService>();
        services.AddScoped<IInternalPurchaseTokenService, InternalPurchaseTokenService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();

        services.AddScoped<ISaleService, SaleService>();

        services.AddScoped<IInternalWalletService, InternalWalletService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<ICouponService, CouponService>();

    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<PurchaseToken>), typeof(CrudDatabaseRepository<PurchaseToken, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
        services.AddScoped(typeof(ICrudRepository<Sale>), typeof(CrudDatabaseRepository<Sale, PaymentsContext>));

        services.AddScoped(typeof(ICrudRepository<Wallet>), typeof(CrudDatabaseRepository<Wallet, PaymentsContext>));
        services.AddScoped<IWalletRepository, WalletDatabaseRepository>();

        services.AddScoped(typeof(ICrudRepository<Coupon>), typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));

        services.AddScoped<IShoppingCartRepository, ShoppingCartDatabaseRepository>();
        services.AddScoped<IPurchaseTokenRepository, PurchaseTokenDatabaseRepository>();
        services.AddScoped<ICouponRepository, CouponDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<PaymentRecord>), typeof(CrudDatabaseRepository<PaymentRecord, PaymentsContext>));

        services.AddScoped<IPaymentRecordRepository, PaymentRecordDatabaseRepository>();

        services.AddScoped<ISaleRepository, SaleDatabaseRepository>();


        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}
