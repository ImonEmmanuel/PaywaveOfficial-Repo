using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaywaveAPICore.Extension
{
    public class PasswordValidatorExtension
    {
        public static bool IsPasswordValid(string password)
        {
            // Check if the password length is greater than 9 characters
            if (password.Length <= 9)
            {
                return false;
            }

            // Check if the password contains at least one uppercase letter, one lowercase letter, one digit, and one special character
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !password.Any(IsSpecialCharacter))
            {
                return false;
            }

            return true;
        }

        private static bool IsSpecialCharacter(char c)
        {
            // Check if the character is a special character
            return !char.IsLetterOrDigit(c);
        }
    }
}
