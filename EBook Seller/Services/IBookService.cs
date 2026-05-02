using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IBookService
    {
        public Task AddAsyncBook(AddBookDTO bookData);
        public Task AddRangeAsyncBook(List<AddBookDTO> bookListData);
    }
}
