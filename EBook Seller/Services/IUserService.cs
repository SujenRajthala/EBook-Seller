using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IUserService
    {
        public Task RegisterUser(RegisterDTO dto);
        public Task<List<GetUsersDTO>> GetUsers();
        public Task DeleteUserById(int id);
        public Task DeleteUser();
    }
}
