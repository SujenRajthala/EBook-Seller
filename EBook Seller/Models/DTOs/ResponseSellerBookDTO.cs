namespace EBook_Seller.Models.DTOs
{
    public class ResponseSellerBookDTO
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string BookDetails { get; set; }
        public string ISBN { get; set; }
        public List<GenreResponseDTO> Genres { get; set; }
        public List<ResponseSellerDTO> Sellers { get; set; }
    }
}
