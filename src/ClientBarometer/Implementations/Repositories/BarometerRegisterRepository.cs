using ClientBarometer.Common.Infrastructure.Repositories;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    class BarometerRegisterRepository : RegisterRepository<BarometerEntry>, IBarometerRegisterRepository
    {
        public BarometerRegisterRepository(DbSet<BarometerEntry> dbSet) : base(dbSet)
        {
        }
    }
}