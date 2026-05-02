using EBook_Seller.Data;
using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(AddBookDTO bookData)
        {
            try
            {
                await _service.AddAsyncBook(bookData);
                return Ok("New Book Added Successfully");
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
            
        }
    }
}
