using EBook_Seller.Data.IRepo;

namespace EBook_Seller.Data.Repo
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BookDbContext _context;
        public UnitOfWork(BookDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
