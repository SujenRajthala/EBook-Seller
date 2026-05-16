namespace EBook_Seller.Models.DTOs
{
    public class ResponseSellerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}
