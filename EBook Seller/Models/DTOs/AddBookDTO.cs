using System.ComponentModel.DataAnnotations;

namespace EBook_Seller.Models.DTOs
{
    public class AddBookDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string ISBN { get; set; }

        public List<int> GenreIds { get; set; }
    }
}
