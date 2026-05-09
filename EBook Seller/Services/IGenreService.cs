using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public interface IGenreService
    {
        public Task AddAsync(AddGenreDTO genre);
        public Task<List<GenreResponseDTO>> GetGenres();
        public Task<GenreResponseDTO> GetGenreById(int id);
        public Task EditGenre(int id, AddGenreDTO editedGenre);
        public Task DeleteGenre(int id);

    }
}
