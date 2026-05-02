using EBook_Seller.Models;

namespace EBook_Seller.Data
{
    public interface IBookRepo
    {
        public Task AddAsyncBook(Book bookData);
        public Task AddRangeAsyncBook(List<Book> bookListData);
        public Task<bool> DoesExist(Book newBook);
    }
}
