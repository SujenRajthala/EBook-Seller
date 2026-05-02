namespace EBook_Seller.Models
{
    public class BookGenre
    {
        public int BookId{ get; set; }
        public int CategoryId{ get; set; }

        public Book? Book { get; set; }
        public Genre? Genre { get; set; }
        
    }
}
