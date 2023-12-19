using AutoMapper;
using Core.Entity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Repository;

public class UserRepository(UserApiDbContext dbContext, IMapper mapper) : RepositoryWithEntityId<UserEntity>(dbContext, mapper);

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<UserEntity> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}