using EBook_Seller.Models;

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
    }
}
