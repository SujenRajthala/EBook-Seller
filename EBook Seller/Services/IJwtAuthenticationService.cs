using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IJwtAuthenticationService
    {
        public Task<LoginRespondDTO> Login(LoginDTO dto);
    }
}
