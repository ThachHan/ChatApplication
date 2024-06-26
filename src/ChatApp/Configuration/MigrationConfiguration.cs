using ChatApp.Persistence.DbManager;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Configuration
{
    public static class MigrationConfiguration
    {
        public static WebApplication MigrateDatabase(this WebApplication webApp)
        {
            using (var scope = webApp.Services.CreateScope())
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (appContext.Database.GetPendingMigrations().Any())
                {
                    appContext.Database.Migrate();
                }
            }
            return webApp;
        }
    }
}
