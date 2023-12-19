using AutoMapper;
using Core;
using Core.Entity;
using Core.RepositoryBase;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;

namespace UserApi.Repository;

public class UserRepository(UserApiDbContext dbContext, IMapper mapper) : RepositoryWithEntityId<User>(dbContext, mapper);

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}