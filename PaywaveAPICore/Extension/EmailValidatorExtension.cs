using System.Text.RegularExpressions;

namespace PaywaveAPICore.Extension
{
    public static class EmailValidatorExtension
    {
        public static bool IsValidEmail(string email)
        {
            // Regular expression pattern for a valid email address
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}