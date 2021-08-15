using ClientBarometer.Common.Abstractions.Repositories;
using ClientBarometer.Domain.Models;

namespace ClientBarometer.Domain.Repositories
{
    public interface IBarometerRegisterRepository : IRegisterRepository<BarometerEntry>
    {
    }
}