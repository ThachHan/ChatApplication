using ChatApp.Domain.Entities;
using ChatApp.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ChatApp.Persistence.DbManager;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    // Add the interceptor
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(b =>
        {
            b.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        });

        modelBuilder.Entity<AppRole>(b =>
        {
            b.Property(x => x.Id).HasDefaultValueSql("newsequentialid()");
        });

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
