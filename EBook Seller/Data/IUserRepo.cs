using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Data
{
    public interface IUserRepo
    {
        public Task RegisterUser(User user);
        public Task Login(LoginDTO dto);
        public Task<List<GetUsersDTO>> GetUsers();
        public Task<List<Role>> GetUserRoles(int id);
        public Task DeleteUserById(int id);
        public Task DeleteUser();

        public Task<bool> EmailExistAsync(string email);
    }
}
