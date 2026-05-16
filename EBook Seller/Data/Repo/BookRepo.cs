using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data.Repo
{
    public class BookRepo : IBookRepo
    {

        private readonly BookDbContext _context;

        public BookRepo(BookDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooks()
        {
            var books =await _context.Books.ToListAsync();
            return books;
        } 

        public async Task<Book> GetBookByName(string bookName)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName);
            if (book == null) return null;
            return book;
        }
        public async Task<Book> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new KeyNotFoundException("There is no data available for this ID");
            return book;
        }

        public async Task AddAsyncBook(Book newBook)
        {
            await _context.Books.AddAsync(newBook);
        }

        public async Task AddRangeAsyncBook(List<Book> bookListData)
        {
            await _context.Books.AddRangeAsync(bookListData);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DuplicateDoesExist(Book newBook)
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

        public async Task<Book> IsEditable(int id, AddBookDTO editedData)
        {
            var exists=await _context.Books.FirstOrDefaultAsync(b => (b.ISBN == editedData.ISBN || b.Name == editedData.Name) && b.Id!= id);
            return exists; 
        }

        public async Task<List<BookByGenreDTO>> GetBookByGenre(int GenreId)
        {
            var books = await _context.Books
                .Where(b => b.BooksGenres.Any(bg => bg.CategoryId == GenreId)).Select(b=>new BookByGenreDTO
                {
                    BookName=b.Name,
                    ISBN=b.ISBN,
                    Genres=b.BooksGenres.Select(bg=>bg.Genre.Name).ToList(),
                })
                .ToListAsync();

            return books;
        }

        public async Task<ResponseSellerBookDTO> GetBooksForCustomer(string bookName)
        {   
            //var books = await _context.Books.Where(b => b.SellerBooks.Any(sb => sb.BookId == bookId)).Select(b => new ResponseSellerBookDTO
            var book = await _context.Books.Where(b => b.Name == bookName).Select(b => new ResponseSellerBookDTO
            {
                Id = b.Id,
                BookName = b.Name,
                BookDetails = b.Details,
                ISBN = b.ISBN,
                Genres = b.BooksGenres.Select(bg => new GenreResponseDTO
                {
                    Id = bg.Genre.Id,
                    Name = bg.Genre.Name,
                    Details = bg.Genre.Details,
                }).ToList(),
                Sellers = b.SellerBooks.Select(bg => new ResponseSellerDTO
                {
                    Id = bg.Seller.Id,
                    Name = bg.Seller.UserName,
                    Quantity = bg.Quantity,
                    Price = bg.Price,
                    Discount = bg.Discount,
                }).ToList(),
            }).FirstOrDefaultAsync();
            return book;
        }
    }
}
