using Infrastructure.Repositories;
using Persistence;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MerchantContext _context;
        public ITransactionRepositories Transaction { get; private set; }

        public UnitOfWork(MerchantContext context)
        {
            _context = context;
            Transaction = new TransactionRepositories(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
