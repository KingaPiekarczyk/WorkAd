using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WorkAd.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<WorkAdvert> WorkAdvert { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var decimalToLongConverter = new ValueConverter<decimal, long>(
                v => (long)Math.Round(v * 100m),
                v => v / 100m
            );

            modelBuilder.Entity<WorkAdvert>(entity =>
            {
                entity.Property(e => e.HourlyRate)
                      .HasConversion(decimalToLongConverter)
                      .HasColumnType("INTEGER")
                      .IsRequired();

                entity.Property(e => e.CreatedDate)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.CreatedDate);
            });

            modelBuilder.Entity<WorkAdvert>().HasData(
                new WorkAdvert
                {
                    Id = 1,
                    Title = "Drugstore salesperson",
                    Description = "We are looking for a salesperson to work in our drugstore. The candidate should have experience in sales and customer service.",
                    HourlyRate = 32.00m,
                    Location = "Warsaw",
                    ContractType = ContractType.ContractOfMandate,
                    WorkType = WorkType.OnSite,
                    CreatedDate = DateTime.UtcNow.AddDays(-5)
                },
                new WorkAdvert
                {
                    Id = 2,
                    Title = "Junior Web Developer",
                    Description = "We are looking for a junior web developer to join our remote development team. Basic knowledge of HTML, CSS, and C# is required.",
                    HourlyRate = 45.50m,
                    Location = "Gdańsk",
                    ContractType = ContractType.B2B,
                    WorkType = WorkType.Remote,
                    CreatedDate = DateTime.UtcNow.AddDays(-12)
                },
                new WorkAdvert
                {
                    Id = 3,
                    Title = "Warehouse Assistant",
                    Description = "A warehouse assistant is needed for packaging, sorting and preparing shipments. No experience required, training provided.",
                    HourlyRate = 37.50m,
                    Location = "Poznań",
                    ContractType = ContractType.ContractForSpecificWork,
                    WorkType = WorkType.OnSite,
                    CreatedDate = DateTime.UtcNow.AddDays(-2)
                }
            );
        }
    }
}
