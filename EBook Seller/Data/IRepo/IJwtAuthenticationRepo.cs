using EBook_Seller.Models;

namespace EBook_Seller.Data.IRepo
{
    public interface IJwtAuthenticationRepo
    {
        public Task<User> Get(string email);
        
    }
}