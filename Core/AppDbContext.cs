﻿using Core.Entity;
using Core.Entity.Identity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace Core;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext,
        IAppDbContext,
        IIdentityDbContext, IUserDbContext {
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<UserUnitEntity> UserUnits { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("Host=localhost;Database=WebApi;Username=rob;Port=5432");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            if (entityType.ClrType.Name.EndsWith("Entity"))
                modelBuilder.Entity(entityType.ClrType)
                    .ToTable(
                        entityType.ClrType.Name.Replace("Entity", "").EndsWith("s")
                            ? entityType.ClrType.Name.Replace("Entity", "")
                            : entityType.ClrType.Name.Replace("Entity", "") + "s");

        base.OnModelCreating(modelBuilder);
    }
}

public interface IUserDbContext : IAppDbContext {
    DbSet<UserEntity> Users { get; set; }
}