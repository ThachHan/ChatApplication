using ChatApp.Domain.Entities;
using ChatApp.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Persistence.DbManager;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

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
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
