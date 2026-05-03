using EBook_Seller.Models;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data
{
    public class BookRepo : IBookRepo
    {

        private readonly BookDbContext _context;

        public BookRepo(BookDbContext context)
        {
            _context = context;
        }

        public async Task AddAsyncBook(Book newBook)
        {
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsyncBook(List<Book> bookListData)
        {
            await _context.Books.AddRangeAsync(bookListData);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DoesExist(Book newBook)
        {
            var exist = await _context.Books.AnyAsync(b=>b.Name== newBook.Name && b.ISBN== newBook.ISBN);
            return exist;
        }

        public async Task<List<Book>> MatchingBooks(List<string> booksName,List<string> booksISBN)
        {
            var existingBook = await _context.Books
                .Where(b => b.ISBN != null && booksISBN.Contains(b.ISBN) || b.Name!=null && booksName.Contains(b.Name))
                .ToListAsync();
            return existingBook;
        }
    }
}
