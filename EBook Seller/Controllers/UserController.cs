using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service=service;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            
            try
            {
                await _service.RegisterUser(dto);
                return Ok("Registration Successful!!");
            }
            catch(InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }

        } 
    }
}
