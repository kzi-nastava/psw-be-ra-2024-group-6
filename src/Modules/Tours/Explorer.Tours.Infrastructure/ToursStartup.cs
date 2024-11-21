using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Internal;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Execution;
using Explorer.Tours.API.Public.Shopping;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Execution;
using Explorer.Tours.Core.UseCases.Shopping;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Object = Explorer.Tours.Core.Domain.Tours.Object;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IPurchaseTokenService, PurchaseTokenService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IObjectService, ObjectService>();
        services.AddScoped<ICheckpointService, CheckpointService>();
        services.AddScoped<ITourService,TourService>();
        services.AddScoped<ITouristEquipmentManagerService, TouristEquipmentManagerService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IInternalProblemTourAuthorService, ProblemTourService>();
        services.AddScoped<IPurchaseTokenService,PurchaseTokenService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Review>), typeof(CrudDatabaseRepository<Review, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Object>), typeof(CrudDatabaseRepository<Object, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, ToursContext>));
        services.AddScoped<IObjectRepository, ObjectDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Checkpoint>), typeof(CrudDatabaseRepository<Checkpoint, ToursContext>));
        services.AddScoped<ITouristEquipmentManagerRepository, TouristEquipmentManagerRepository>();
        services.AddScoped<ICheckpointRepository, CheckpointDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<PurchaseToken>), typeof(CrudDatabaseRepository<PurchaseToken, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour,ToursContext>));

        services.AddScoped<ITouristEquipmentManagerRepository, TouristEquipmentManagerRepository>();
        services.AddScoped<IReviewRepository, ReviewDatabaseRepository>();
        services.AddScoped<ITourRepository, TourDatabaseRepository>();
        services.AddScoped<ITourExecutionRepository, TourExecutionDatabaseRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartDatabaseRepository>();
        services.AddScoped<IPurchaseTokenRepository, PurchaseTokenDatabaseRepository>();
        services.AddScoped<ITourEquipmentRepository, TourEquipmentRepository>();


        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}