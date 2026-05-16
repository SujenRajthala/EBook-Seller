using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data.IRepo
{
    public interface IBookRepo
    {
        public Task<List<Book>> GetBooks();
        public Task<Book> GetBookByName(string bookName);
        public Task<Book> GetBookById(int id);
        public Task<List<BookByGenreDTO>> GetBookByGenre(int GenreId);
        public Task<ResponseSellerBookDTO> GetBooksForCustomer(string bookName);
        public Task AddAsyncBook(Book bookData);
        public Task AddRangeAsyncBook(List<Book> bookListData);
        public Task<bool> DuplicateDoesExist(Book newBook);
        public Task<List<Book>> MatchingBooks(List<string> booksName, List<string> booksISBN);
        public Task<Book> IsEditable(int id, AddBookDTO editedData);
    }
}
