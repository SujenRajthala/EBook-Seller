namespace EBook_Seller.Models
{
    public class SellerBook
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public User Seller { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }


    }
}
