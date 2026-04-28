using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly BookDbContext _context;
        public RefreshTokenRepo(BookDbContext context)
        {
            _context = context;
        }
        public async Task AddRefreshToken(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> VerifyRefreshToken(RequestRefreshDTO request)
        {
            var refreshToken = await _context.RefreshTokens.Include(rf=>rf.User).FirstOrDefaultAsync(rf => rf.Token == request.RefreshToken);
            return refreshToken;
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public async Task<string> HasLoggedIn(User user)
        {
            var hasEntry = await _context.RefreshTokens
                .Where(rt => rt.UserId == user.Id)
                .Select(rt => rt.Token).FirstOrDefaultAsync();
            return hasEntry;
        }

    }
}
