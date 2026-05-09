using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IBookService
    {
        public Task<List<BookResponseDTO>> GetBooks();
        public Task<BookResponseDTO> GetBookByName(string bookName);
        public Task<BookResponseDTO> GetBookById(int id);
        public Task AddAsyncBook(AddBookDTO bookData);
        public Task AddRangeAsyncBook(List<AddBookDTO> bookListData);
        public Task EditBook(int id,AddBookDTO bookEdit);
    }
}
