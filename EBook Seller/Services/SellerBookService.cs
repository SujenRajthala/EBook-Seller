using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using System.Security.Claims;


namespace EBook_Seller.Services
{
    public class SellerBookService : ISellerBookService
    {
        private readonly ISellerBookRepo _repo;
        private readonly IBookRepo _bookRepo;

        public SellerBookService(ISellerBookRepo repo, IBookRepo bookRepo)
        {
            _repo = repo;
            _bookRepo = bookRepo;
        }

        public async Task CreateSellerBook(AddSellerBookDTO newData,int sellerId)
        {
            await _bookRepo.GetBookById(newData.BookId);
            var readyData = new SellerBook
            {
                SellerId = sellerId,
                BookId = newData.BookId,
                Price = newData.Price,
                Quantity = newData.Quantity,
                Discount = newData.Discount,
            };
            await _repo.CreateSellerBook(readyData);
        }
    }
}
