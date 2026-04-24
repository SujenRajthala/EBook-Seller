using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Data
{
    public interface IUserRepo
    {
        public Task RegisterUser(User user);
        public Task Login(LoginDTO dto);
        public Task<List<User>> GetUser();
        public Task DeleteUserById(int id);
        public Task DeleteUser();

        public Task<bool> EmailExistAsync(string email);
    }
}
