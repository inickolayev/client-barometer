using ClientBarometer.Common.Abstractions.UnitOfWork;
using ClientBarometer.Domain.Repositories;

namespace ClientBarometer.Domain.UnitsOfWork
{
    public interface IChatUnitOfWork : IUnitOfWork
    {
        IMessageRegisterRepository Messages { get; }
        IChatRegisterRepository Chats { get; }
        IUserRegisterRepository Users { get; }
        IBarometerRegisterRepository BarometerValues { get; }
    }
}