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

        public Task<List<User>> GetUser()
        {
            throw new NotImplementedException();
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
