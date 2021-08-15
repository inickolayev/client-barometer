using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.Common.Abstractions.UnitOfWork;
using ClientBarometer.DataAccess;
using ClientBarometer.Domain.Models;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.UnitsOfWork;
using Microsoft.EntityFrameworkCore;

namespace ClientBarometer.Implementations.Repositories
{
    public class BarometerReadRepository : IBarometerReadRepository
    {
        private readonly IQueryable<BarometerEntry> _barometerValues;

        public BarometerReadRepository(ClientBarometerDbContext dbContext)
        {
            _barometerValues = dbContext.BarometerEntries.AsNoTracking();
        }

        public async Task<BarometerEntry> Get(Guid chatId, CancellationToken cancellationToken)
            => await _barometerValues.FirstOrDefaultAsync(bv => bv.ChatId == chatId, cancellationToken);

        public async Task<bool> Contains(Guid chatId, CancellationToken cancellationToken)
            => await _barometerValues.AnyAsync(bv => bv.ChatId == chatId, cancellationToken);
    }
}