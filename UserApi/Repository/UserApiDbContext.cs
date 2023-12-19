using Core;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace UserApi;

public class UserApiDbContext(DbContextOptions<UserApiDbContext> options) : DbContext(options) {
    public DbSet<User> Users { get; set; }
    public DbSet<UserUnit> UserUnits { get; set; }
}