using EBook_Seller.Models;
using Microsoft.AspNetCore.Identity;

namespace EBook_Seller.Handler
{
    public class PasswordHashHandler
    {
        public static bool VerifyUserPassword(User user, string providedPassword)
        {
            var passwordHasher = new PasswordHasher<User>();

            // This method compares the plain-text password with the stored hash
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);

            return result switch
            {
                PasswordVerificationResult.Success => true,
                PasswordVerificationResult.SuccessRehashNeeded => true, // Valid, but consider updating the hash
                PasswordVerificationResult.Failed => false,
                _ => false
            };
        }
    }
}
