using EBook_Seller.Models;

namespace EBook_Seller.Data
{
    public class RoleRepo : IRoleRepo
    {
        private readonly BookDbContext _context;
        public RoleRepo(BookDbContext context)
        {
            _context = context;
        }
        public async Task AddRoleAsync(Role newRole)
        {
            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();
        }

        public Task DeleteRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}
