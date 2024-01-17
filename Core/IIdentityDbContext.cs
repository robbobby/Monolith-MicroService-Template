using Core.Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core;

public interface IIdentityDbContext {
    DbSet<UserEntity> Users { get; set; }
}
