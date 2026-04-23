using EBook_Seller.Models;

namespace EBook_Seller.Data
{
    public interface IRoleRepo
    {
        public Task AddRoleAsync(Role newRole);
        public Task DeleteRole(int id);
    }
}
