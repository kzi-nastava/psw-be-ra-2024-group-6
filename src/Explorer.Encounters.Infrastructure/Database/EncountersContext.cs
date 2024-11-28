using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Encounters.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext : DbContext
    {
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<TouristRank> TouristRanks { get; set; }

        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");
            modelBuilder.Entity<SocialEncounter>()
                .ToTable("SocialEncounters")
                .HasBaseType<Encounter>();
            modelBuilder.Entity<HiddenEncounter>()
                .ToTable("HiddenEncounters")
                .HasBaseType<Encounter>();

            ConfigureEncounter(modelBuilder);
            ConfigureTouristRank(modelBuilder);
        }

        private void ConfigureEncounter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encounter>(entity =>
            {

                entity.Property(e => e.Location).HasColumnType("jsonb");

            });
            modelBuilder.Entity<HiddenEncounter>(entity =>
            {
                entity.Property(e => e.HiddenLocation).HasColumnType("jsonb");
            });


        }

        private void ConfigureTouristRank(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TouristRank>().HasIndex(tr => tr.TouristId).IsUnique();
        }
    }
}
