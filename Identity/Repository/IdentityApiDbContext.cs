using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Repository;

public class IdentityApiDbContext : IdentityDbContext<IdentityUser> {
    public IdentityApiDbContext(DbContextOptions<IdentityApiDbContext> options) : base(options) { }
}
