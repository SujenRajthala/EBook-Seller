using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data
{
    public interface IBookRepo
    {
        public Task<Book> GetBook(string bookName);
        public Task AddAsyncBook(Book bookData);
        public Task AddRangeAsyncBook(List<Book> bookListData);
        public Task<bool> DoesExist(Book newBook);
        public Task<List<Book>> MatchingBooks(List<string> booksName, List<string> booksISBN);
        public Task EditBook(int id, AddBookDTO bookEdit);
    }
}
