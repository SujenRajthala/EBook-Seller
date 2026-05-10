namespace EBook_Seller.Models.DTOs
{
    public class BookByGenreDTO
    {
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public List<string> Genres { get; set; }
    }
}
