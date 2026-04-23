using EBook_Seller.Data;
using EBook_Seller.Models;

namespace EBook_Seller.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepo _repo;
        public RoleService(IRoleRepo repo)
        {
            _repo = repo;
        }
        public async Task<Role> AddRoleAsync(string Name)
        {
            var newRole = new Role
            {
                RoleName = Name
            };
            await _repo.AddRoleAsync(newRole);
            return newRole;
        }

        public async Task DeleteRole(int id)
        {
            
        }
    }
}
