using EBook_Seller.Data.IRepo;
using EBook_Seller.Migrations;
using EBook_Seller.Models;

namespace EBook_Seller.Data.Repo
{
    public class SellerBookRepo : ISellerBookRepo
    {
        private readonly BookDbContext _context;

        public SellerBookRepo(BookDbContext context)
        {
            _context=context;
        }

        public async Task CreateSellerBook(SellerBook newData)
        {
            await _context.SellerBooks.AddAsync(newData);
            await _context.SaveChangesAsync();
        }
    }
}
