﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Tests;
using Explorer.Encounters.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Encounters.Tests
{
    public class EncountersTestFactory : BaseTestFactory<EncountersContext>
    {

        protected override IServiceCollection ReplaceNeededDbContexts(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EncountersContext>));
            services.Remove(descriptor!);
            services.AddDbContext<EncountersContext>(SetupTestContext());

            /*
            descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<OTHER_MODULE_NAMEContext>));
            services.Remove(descriptor!);
            services.AddDbContext<OTHER_MODULE_NAMEContext>(SetupTestContext());
            */

            return services;
        }

    }
}
