
using Microsoft.EntityFrameworkCore;
using WebApplication7.Models;

namespace WebApplication7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pc> Pcs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<PcComponent> PcComponents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pc>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10,2)");

            entity.Property(e => e.Warranty)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.Stock)
                .IsRequired();
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);
        });

        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Abbreviation)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                .HasMaxLength(10);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Description)
                .HasMaxLength(250);

            entity.HasOne(e => e.Manufacturer)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.ManufacturerId);

            entity.HasOne(e => e.Type)
                .WithMany(e => e.Components)
                .HasForeignKey(e => e.TypeId);
        });

        modelBuilder.Entity<PcComponent>(entity =>
        {
            entity.HasKey(e => new { e.PcId, e.ComponentCode });

            entity.Property(e => e.Amount)
                .IsRequired();

            entity.HasOne(e => e.Pc)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.PcId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Component)
                .WithMany(e => e.PcComponents)
                .HasForeignKey(e => e.ComponentCode);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>().HasData(
            new Manufacturer
            {
                Id = 1,
                Abbreviation = "AMD",
                FullName = "Advanced Micro Devices",
                FoundationDate = new DateOnly(1969, 5, 1)
            },
            new Manufacturer
            {
                Id = 2,
                Abbreviation = "NV",
                FullName = "NVIDIA Corporation",
                FoundationDate = new DateOnly(1993, 4, 5)
            },
            new Manufacturer
            {
                Id = 3,
                Abbreviation = "COR",
                FullName = "Corsair Gaming Inc.",
                FoundationDate = new DateOnly(1994, 1, 1)
            }
        );

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType
            {
                Id = 1,
                Abbreviation = "CPU",
                Name = "Processor"
            },
            new ComponentType
            {
                Id = 2,
                Abbreviation = "GPU",
                Name = "Graphics Card"
            },
            new ComponentType
            {
                Id = 3,
                Abbreviation = "RAM",
                Name = "Memory"
            }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component
            {
                Code = "CPU0000001",
                Name = "Ryzen 7 7800X3D",
                Description = "8-core gaming processor",
                ManufacturerId = 1,
                TypeId = 1
            },
            new Component
            {
                Code = "GPU0000001",
                Name = "RTX 4080 Super",
                Description = "High-end gaming graphics card",
                ManufacturerId = 2,
                TypeId = 2
            },
            new Component
            {
                Code = "RAM0000001",
                Name = "Corsair Vengeance DDR5 16GB",
                Description = "DDR5 RAM module 16GB",
                ManufacturerId = 3,
                TypeId = 3
            }
        );

        modelBuilder.Entity<Pc>().HasData(
            new Pc
            {
                Id = 1,
                Name = "Gaming Beast X",
                Weight = 12.5m,
                Warranty = 36,
                CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0),
                Stock = 5
            },
            new Pc
            {
                Id = 2,
                Name = "Office Mini Pro",
                Weight = 4.2m,
                Warranty = 24,
                CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0),
                Stock = 12
            },
            new Pc
            {
                Id = 3,
                Name = "Student Basic PC",
                Weight = 6.8m,
                Warranty = 12,
                CreatedAt = new DateTime(2026, 3, 20, 10, 15, 0),
                Stock = 20
            }
        );

        modelBuilder.Entity<PcComponent>().HasData(
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "CPU0000001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "GPU0000001",
                Amount = 1
            },
            new PcComponent
            {
                PcId = 1,
                ComponentCode = "RAM0000001",
                Amount = 2
            }
        );
    }
}