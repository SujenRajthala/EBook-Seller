using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Seller")]
    public class SellerBookController : ControllerBase
    {
        private readonly ISellerBookService _service;

        public SellerBookController(ISellerBookService service)
        {
            _service=service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSellerBook(AddSellerBookDTO newData)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userId, out int sellerId)) return Unauthorized();
                await _service.CreateSellerBook(newData, sellerId);
                return Ok("Book for your store is added in System.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }
    }
}
