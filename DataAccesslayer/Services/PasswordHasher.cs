using System;
using System.Security.Cryptography;
using System.Text;

namespace DataAccesslayer.Services
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            // Use a fixed salt for consistency across machines
            string salt = "FixedSaltValue";

            // Create a SHA256 hash algorithm instance
            using (SHA256 sha256 = SHA256.Create())
            {
                // Combine the password and salt
                var combinedPasswordSalt = password + salt;

                // Convert the combined string to bytes and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPasswordSalt));

                // Convert the byte array to a Base64 string and return
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
