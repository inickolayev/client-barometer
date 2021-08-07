using ClientBarometer.Common.Infrastructure.Repositories;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    class ChatRegisterRepository : RegisterRepository<Chat>, IChatRegisterRepository
    {
        public ChatRegisterRepository(DbSet<Chat> dbSet) : base(dbSet)
        {
        }
    }
}