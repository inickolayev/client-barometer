using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClientBarometer.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext<TDbContext>(this IServiceCollection services,
            string connectionString)
            where TDbContext : DbContext
            => services.AddDbContextPool<TDbContext>(
                options =>
                {
                    options.UseMySql(connectionString,
                        ServerVersion.AutoDetect(connectionString),
                        b => { b.EnableRetryOnFailure(2); });
                    // options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
                });
    }
}