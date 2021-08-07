using ClientBarometer.Common.Infrastructure.Repositories;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    class UserRegisterRepository : RegisterRepository<User>, IUserRegisterRepository
    {
        public UserRegisterRepository(DbSet<User> dbSet) : base(dbSet)
        {
        }
    }
}