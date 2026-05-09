using EBook_Seller.Models;

namespace EBook_Seller.Data.IRepo
{
    public interface IBookGenreRepo
    {
        public Task AddBookGenre(List<BookGenre> bookGenres);
    }
}
