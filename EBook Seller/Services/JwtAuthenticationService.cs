using EBook_Seller.Data;
using EBook_Seller.Handler;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace EBook_Seller.Services
{
    public class JwtAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtAuthenticationRepo _jwtRepo;

        public JwtAuthenticationService(IConfiguration configuration,IJwtAuthenticationRepo jwtRepo)
        {
            _configuration = configuration;
            _jwtRepo = jwtRepo;
        }

        public async Task<LoginRespondDTO> Login(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password)) return null;
            var user = await _jwtRepo.Get(dto.Email);

            if (user is null || !PasswordHashHandler.VerifyUserPassword(user, dto.Password)) return null;
            return new LoginRespondDTO();
        }

    }
}
