using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IRoleService
    {
        public Task<Role> AddRoleAsync(string Name);
        public Task AssignRoleAsync(AssignRoleDTO userRole);
        public Task DeleteRole(int id);
    }
}
