using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesslayer.Services
{
    public class PasswordGenerator
    {
        public string Password { get; set; }

        public string GetPassword()
        {
            Password = GenerateRandomString(8, 15);
            return Password;
        }

        static string GenerateRandomString(int minLength, int maxLength)
        {
            // Define the characters to use in the random string
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+[]{}|;:,.<>?";
            const string allChars = upperChars + lowerChars + digitChars + specialChars;

            // Create a new instance of the Random class
            Random random = new Random();

            // Generate a random length between minLength and maxLength
            int length = random.Next(minLength, maxLength + 1);

            // Use a StringBuilder for efficient string concatenation
            StringBuilder result = new StringBuilder(length);

            // Ensure the string contains at least one of each required character type
            result.Append(upperChars[random.Next(upperChars.Length)]);
            result.Append(lowerChars[random.Next(lowerChars.Length)]);
            result.Append(digitChars[random.Next(digitChars.Length)]);
            result.Append(specialChars[random.Next(specialChars.Length)]);

            // Fill the rest of the string with random characters
            for (int i = 4; i < length; i++)
            {
                result.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the result to avoid predictable patterns
            return ShuffleString(result.ToString(), random);
        }

        static string ShuffleString(string input, Random random)
        {
            char[] array = input.ToCharArray();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // Swap array[i] with array[j]
                char temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            return new string(array);
        }
    }
}
