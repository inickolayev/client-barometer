using ClientBarometer.Common.Infrastructure.Repositories;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    class MessageRegisterRepository : RegisterRepository<Message>, IMessageRegisterRepository
    {
        public MessageRegisterRepository(DbSet<Message> dbSet) : base(dbSet)
        {
        }
    }
}