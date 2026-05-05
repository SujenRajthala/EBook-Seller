using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
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
            var exist = await _context.Books
                .AsNoTracking()
                .AnyAsync(b=>b.Name== newBook.Name && b.ISBN== newBook.ISBN);
            return exist;
        }

        public async Task<List<Book>> MatchingBooks(List<string> booksName,List<string> booksISBN)
        {
            var existingBook = await _context.Books
                .AsNoTracking()
                .Where(b => b.ISBN != null && booksISBN.Contains(b.ISBN) || b.Name!=null && booksName.Contains(b.Name))
                .ToListAsync();
            return existingBook;
        }

        public async Task EditBook(int id, AddBookDTO bookEdit)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null) throw new InvalidOperationException("There is no data available for this ID");
            book.Name = bookEdit.Name;
            book.Details = bookEdit.Details;
            book.ISBN = bookEdit.ISBN;

            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBook(string bookName)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName);
            if (book == null) throw new InvalidOperationException("There is no data available for this ID");
            return book;
        }
    }
}
