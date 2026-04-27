using EBook_Seller.Data;
using EBook_Seller.Handler;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EBook_Seller.Services
{
    public class JwtAuthenticationService: IJwtAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtAuthenticationRepo _jwtRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRefreshTokenRepo _refreshRepo;

        public JwtAuthenticationService(IConfiguration configuration,IJwtAuthenticationRepo jwtRepo, IUserRepo userRepo,IRefreshTokenRepo refreshRepo)
        {
            _configuration = configuration;
            _jwtRepo = jwtRepo;
            _userRepo = userRepo;
            _refreshRepo = refreshRepo;
        }

        public async Task<LoginRespondDTO> Login(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password)) return null;
            var user = await _jwtRepo.Get(dto.Email);

            if (user is null || !PasswordHashHandler.VerifyUserPassword(user, dto.Password)) return null;

            var respond = await GenerateToken(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = respond.RefreshToken,
                UserId = user.Id,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            await _refreshRepo.AddRefreshToken(refreshToken);

            return respond;
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        private async Task<LoginRespondDTO> GenerateToken(User user)
        {

            var userRoles = await _userRepo.GetUserRoles(user.Id);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireTime = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JwtConfig:TokenValidityMins"));
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email)
            };

            //Adds Multiple Role to Role Claim if their are are multiple role
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.RoleName)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = credential,
                Expires = expireTime
            };

            var tokenHandler = new JsonWebTokenHandler();
            string token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoginRespondDTO { UserName = user.UserName, AccessToken = token, ExpireIn = expireTime,RefreshToken= GenerateRefreshToken() };
        }

    }
}
