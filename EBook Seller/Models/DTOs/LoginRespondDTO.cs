namespace EBook_Seller.Models.DTOs
{
    public class LoginRespondDTO
    {
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public int ExpireIn { get; set; }
        public string? RefreshToken { get; set; }
    }
}
