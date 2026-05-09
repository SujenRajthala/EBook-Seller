using EBook_Seller.Data.IRepo;
using EBook_Seller.Models;
using Microsoft.EntityFrameworkCore;

namespace EBook_Seller.Data.Repo
{
    public class GenreRepo: IGenreRepo
    {
        private readonly BookDbContext _context;

        public GenreRepo(BookDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Genre newGenre)
        {
            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGenre(int id)
        {
            var toBeDeleted = await GetGenreById(id);
            if (toBeDeleted == null) throw new KeyNotFoundException("There is no data for the provided Id.");
            _context.Genres.Remove(toBeDeleted);
            await _context.SaveChangesAsync();

        }

        public async Task<Genre> GetGenreById(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g=>g.Id==id);
            return genre;
        }

        public async Task<List<Genre>> GetGenres()
        {
            var genres = await _context.Genres.AsNoTracking().ToListAsync();
            return genres;
        }

        public async Task<bool> ValidateDuplicates(string genreName)
        {
            var doesExists = await _context.Genres.AsNoTracking().AnyAsync(g => g.Name == genreName);

            return doesExists;
        }
        public async Task<List<int>> ValidateGenreExist(List<int> genres)
        {
            var existingGenre = await _context.Genres.Where(g => genres.Contains(g.Id)).Select(g=>g.Id).ToListAsync();
            var noGenre = genres.Where(g => !existingGenre.Contains(g)).ToList();

            if (!noGenre.Any()) return null;

            return noGenre;
        }
    }
}
