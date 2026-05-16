using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;
using EBook_Seller.Models.DTOs;

namespace EBook_Seller.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepo _repo;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IGenreRepo repo,IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(AddGenreDTO genre)
        {
            if (await _repo.ValidateDuplicates(genre.Name)) 
                throw new InvalidOperationException("This Genre Already Exists in System!!");
            var newGenre = new Genre
            {
                Name = genre.Name,
                Details = genre.Details,
            };

            await _repo.AddAsync(newGenre);
        }

        public async Task DeleteGenre(int id)
        {
            
            await _repo.DeleteGenre(id); 
        }

        public async Task EditGenre(int id, AddGenreDTO editedGenre)
        {
            var oldData = await _repo.GetGenreById(id);
            if (oldData == null) throw new KeyNotFoundException("There is no data for the provided Id");
            var genres = await _repo.GetGenres();
            if (genres.Any(g=>g.Name==editedGenre.Name && g.Id!=oldData.Id)) throw new InvalidOperationException($"There is already a Genre Naming: {editedGenre.Name}");
            oldData.Name = editedGenre.Name;
            oldData.Details = editedGenre.Details;
           await _unitOfWork.SaveChangesAsync();
        }

        public async Task<GenreResponseDTO> GetGenreById(int id)
        {
            var genre = await _repo.GetGenreById(id);
            if (genre == null) return null;
            var cleanedGenre = new GenreResponseDTO {Id=genre.Id, Details = genre.Details, Name = genre.Name };
            return cleanedGenre;
        }

        public async Task<List<GenreResponseDTO>> GetGenres()
        {
            var listOfGenres=await _repo.GetGenres();
            var cleanedGenre = listOfGenres
                .Select(g => new GenreResponseDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Details = g.Details
                }).ToList();

            return cleanedGenre;
        }
    }
}
