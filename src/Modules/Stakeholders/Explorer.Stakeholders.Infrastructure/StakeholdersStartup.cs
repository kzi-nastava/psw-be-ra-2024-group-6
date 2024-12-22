using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Explorer.Stakeholders.API.Public.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.Core.Domain.Persons;
using Explorer.Stakeholders.Core.Domain.Problems;
using Explorer.Tours.API.Internal;
using Explorer.Stakeholders.API.Internal;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IProblemService, ProblemService>();

        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IInternalInstructorService, InternalInstructorService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IInternalTourPersonService, InternalTourPersonService>();
        services.AddScoped<IInternalUserPaymentService, InternalUserPaymentService>();
        services.AddScoped<IInternalUserService, InternalUserService>();
        services.AddScoped<IInternalNotificationService, InternalNotificationService>();
        services.AddScoped<IInternalPersonService, InternalPersonService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Rating>), typeof(CrudDatabaseRepository<Rating, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Problem>), typeof(CrudDatabaseRepository<Problem, StakeholdersContext>));

        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        services.AddScoped<IRatingRepository, RatingDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User,StakeholdersContext>));
        services.AddScoped<INotificationRepository, NotificationDatabaseRepository>(); 
        services.AddScoped<IProblemRepository, ProblemRepository>();


        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}