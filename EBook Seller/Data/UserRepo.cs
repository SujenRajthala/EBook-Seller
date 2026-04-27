using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly BookDbContext _context;

        public UserRepo(BookDbContext context)
        {
            _context = context;
        }


        public async Task RegisterUser(User user)
        {
           await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public Task Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetUsersDTO>> GetUsers()
        {
            var users =await _context.Users.Select(u => new GetUsersDTO
            {
                UserName = u.UserName,
                UserRoles = u.UserRoles.Select(ur=>ur.Role.RoleName).ToList()
            }).ToListAsync();
            return users;
        }
        public async Task<List<Role>> GetUserRoles(int id)
        {
            var roles = await _context.UserRoles.Where(ur => ur.UserId == id).Select(ur=>ur.Role).ToListAsync();
            return roles;
        }
        public Task DeleteUser()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
            
        }
    }
}
