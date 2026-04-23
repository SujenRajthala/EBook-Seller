using EBook_Seller.Models;

namespace EBook_Seller.Services
{
    public interface IRoleService
    {
        public Task<Role> AddRoleAsync(string Name);
        public Task DeleteRole(int id);
    }
}
