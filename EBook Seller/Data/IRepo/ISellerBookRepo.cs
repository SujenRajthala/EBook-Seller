using EBook_Seller.Migrations;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Data.IRepo
{
    public interface ISellerBookRepo
    {
        public Task CreateSellerBook(SellerBook newData);
        
    }
}
