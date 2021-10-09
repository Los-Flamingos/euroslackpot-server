using System.Text.RegularExpressions;

namespace Common.Helpers
{
    public static class PhoneNumberHelper
    {
        public static bool IsValidSwedishPhoneNumber(string phoneNumber)
        {
            var regex = new Regex(@"^((((0{2}?)|(\+){1})46)|0)7[\d]{8}");
            return regex.IsMatch(phoneNumber);
        }
    }
}
