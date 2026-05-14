using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface ISellerBookService
    {
        public Task CreateSellerBook(AddSellerBookDTO newData, int sellerId);
    }
}
