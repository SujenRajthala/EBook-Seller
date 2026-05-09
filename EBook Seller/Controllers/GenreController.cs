using EBook_Seller.Data;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;
using EBook_Seller.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBook_Seller.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service)
        {
            _service= service;
        }

        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenre(AddGenreDTO genre)
        {
            try
            {
                if (genre != null)
                {
                    await _service.AddAsync(genre);
                    return Ok("Your Genre Data is Created.");
                }
                return BadRequest("Please provide proper information");
            }catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpGet("ListGenre")]
        public async Task<IActionResult> GetGenres()
        {
            var genres =await _service.GetGenres();
            return Ok(genres);
        }

        [HttpGet("GetGenreById/{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre =await _service.GetGenreById(id);
            if (genre == null) return NoContent();
            return Ok(genre);
        }


        [HttpPut("EditGenre/{id}")]
        public async Task<IActionResult> EditGenre(int id,AddGenreDTO editedData)
        {
            try
            {
                await _service.EditGenre(id, editedData);
                return Ok("Your Data has being successfully updated.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("DeleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _service.DeleteGenre(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            

        } 

    }
}
