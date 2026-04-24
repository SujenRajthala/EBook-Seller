using EBook_Seller.Models;

namespace EBook_Seller.Data
{
    public class JwtAuthenticationRepo : IJwtAuthenticationRepo
    {
        private readonly BookDbContext _context;

        public JwtAuthenticationRepo(BookDbContext context)
        {
            _context = context;
        }
        public async Task<User> Get(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;
            return user;
        }
    }
}
