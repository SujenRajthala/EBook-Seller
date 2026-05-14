namespace EBook_Seller.Models.DTOs
{
    public class AddSellerBookDTO
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}
