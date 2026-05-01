using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IJwtAuthenticationService
    {
        public Task<LoginRespondDTO> Login(LoginDTO dto);
        public Task<string> Login1(LoginDTO dto);
        public Task<LoginRespondDTO> GetRefreshedToken(RequestRefreshDTO request);
    }
}
