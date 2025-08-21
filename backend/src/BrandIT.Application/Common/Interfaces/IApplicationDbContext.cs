using BrandIT.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrandIT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Service> Services { get; }
    DbSet<TeamMember> TeamMembers { get; }
    DbSet<BlogPost> BlogPosts { get; }
    DbSet<ContactMessage> ContactMessages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
