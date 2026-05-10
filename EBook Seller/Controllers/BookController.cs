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
    [Authorize(Roles = "Admin")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _service.GetBooks();
            return Ok(books);
        }

        [HttpGet("GetBookByName/{bookName}")]
        public async Task<IActionResult> GetBook(string bookName)
        {
            
                if (bookName == null) return BadRequest("Enter the Book Name!!");
                var book = await _service.GetBookByName(bookName);
                if (book == null) return NotFound($"There is no any Book Name {bookName}.");
                return Ok(book);
            
        }

        [HttpGet("GetBookById/{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
                var book = await _service.GetBookById(id);
                if (book == null) return NotFound($"There is no any Book Name with Id: {id}.");
                return Ok(book);
            
        }

        [HttpGet("GetBookByGenre/{genreId}")]
        public async Task<IActionResult> GetBookByGenre(int genreId)
        {
                var book = await _service.GetBookByGenre(genreId);
                if (book == null) return NotFound($"There is no any Book with Genre Id: {genreId}.");
                return Ok(book);
            
        }

        [Authorize(Roles = "Admin")]
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
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddBooks")]
        public async Task<IActionResult> AddBooks(AddListBookDTO booksData)
        {
            try
            {
                await _service.AddRangeAsyncBook(booksData.Books);
                return Ok("Your List of books are saved to the System.");
            }catch(InvalidOperationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("EditBook/{id}")]
        public async Task<IActionResult> EditBook(int id,AddBookDTO editedBookData)
        {
            try
            {
                await _service.EditBook(id, editedBookData);
                return Ok("Your data has been successfully edited");
            }catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }catch(KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }catch(Exception ex)
            {
                return StatusCode(500,"An internal error occured!!");
            }
           
        }
    }
}
