using System.Threading;
using System.Threading.Tasks;
using ClientBarometer.DataAccess;
using ClientBarometer.Domain.Repositories;
using ClientBarometer.Domain.UnitsOfWork;
using ClientBarometer.Implementations.Repositories;

namespace ClientBarometer.Implementations.UnitsOfWork
{
    public class ChatUnitOfWork : IChatUnitOfWork
    {
        private readonly ClientBarometerDbContext _dbContext;
        public IMessageRegisterRepository Messages { get; }
        public IChatRegisterRepository Chats { get; }
        public IUserRegisterRepository Users { get; }
        public IBarometerRegisterRepository BarometerValues { get; }
        
        public ChatUnitOfWork(ClientBarometerDbContext dbContext)
        {
            _dbContext = dbContext;
            Messages = new MessageRegisterRepository(dbContext.Messages);
            Chats = new ChatRegisterRepository(dbContext.Chats);
            Users = new UserRegisterRepository(dbContext.Users);
            BarometerValues = new BarometerRegisterRepository(dbContext.BarometerEntries);
        }
        
        public async Task Complete(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}