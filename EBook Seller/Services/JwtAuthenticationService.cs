using EBook_Seller.Data;
using EBook_Seller.Handler;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        private async Task<LoginRespondDTO> GenerateToken(User user)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidityMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var token=new JwtSecurityToken(issuer, audience, [
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                new Claim()
                ])
        }

    }
}
