using EBook_Seller.Data;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

        public async Task AssignRoleAsync(AssignRoleDTO userRole)
        {
            await _repo.AssignRoleAsync(new UserRole { UserId=userRole.UserId,RoleId=userRole.RoleId });
        }

        public async Task DeleteRole(int id)
        {
            
        }
    }
}
