using EBook_Seller.Models;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data.IRepo
{
    public interface IGenreRepo
    {
        public Task AddAsync(Genre newGenre);

        public Task<bool> ValidateDuplicates(string genreName);

        public Task<List<Genre>> GetGenres();
        public Task<Genre> GetGenreById(int id);
        public Task DeleteGenre(int id);

        public Task<List<int>> ValidateGenreExist(List<int> genres);
    }
}
