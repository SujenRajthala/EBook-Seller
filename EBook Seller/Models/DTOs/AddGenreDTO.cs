using System.ComponentModel.DataAnnotations;

namespace EBook_Seller.Models.DTOs
{
    public class AddGenreDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }
    }
}
