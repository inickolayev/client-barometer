using ClientBarometer.DataAccess.Maps;
using ClientBarometer.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.DataAccess
{
    public class ClientBarometerDbContext : DbContext
    {
        public ClientBarometerDbContext(DbContextOptions<ClientBarometerDbContext> options)
            : base(options){}

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<BarometerEntry> BarometerEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Build<Message>();
            builder.Build<User>();
            builder.Build<Chat>();
            builder.Build<BarometerEntry>();
        }
    }
}