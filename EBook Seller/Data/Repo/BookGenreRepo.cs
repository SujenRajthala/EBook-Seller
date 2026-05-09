using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;

namespace EBook_Seller.Data.Repo
{
    public class BookGenreRepo : IBookGenreRepo
    {

        private readonly BookDbContext _context;

        public BookGenreRepo(BookDbContext context)
        {
            _context = context;
        }
        public async Task AddBookGenre(List<BookGenre> bookGenres)
        {
            await _context.BooksCategories.AddRangeAsync(bookGenres);
            await _context.SaveChangesAsync();
        }
    }
}
