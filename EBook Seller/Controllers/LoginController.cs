using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IJwtAuthenticationService _service;

        public LoginController(IJwtAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO credentials)
        {
            var tokenResponse = await _service.Login(credentials);
            return Ok(tokenResponse);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RequestRefreshDTO request)
        {
            var respond = await _service.GetRefreshedToken(request);
            return Ok(respond);
        }
    }
}
