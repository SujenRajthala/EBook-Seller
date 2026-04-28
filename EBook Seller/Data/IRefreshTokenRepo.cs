using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Data
{
    public interface IRefreshTokenRepo
    {
        public Task AddRefreshToken(RefreshToken refreshToken);
        public Task<RefreshToken> VerifyRefreshToken(RequestRefreshDTO request);
        public Task SaveChangesAsync();
        public Task<string> HasLoggedIn(User user);
    }
}
