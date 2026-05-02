namespace EBook_Seller.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Details { get; set; }
        public List<BookGenre>? BooksGenres { get; set; }
    }
}
