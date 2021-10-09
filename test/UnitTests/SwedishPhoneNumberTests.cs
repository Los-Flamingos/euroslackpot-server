using Common.Helpers;
using Xunit;

namespace UnitTests
{
    public class SwedishPhoneNumberTests
    {
        [Theory]
        [InlineData("0727183919", true)]
        [InlineData("+46727183919", true)]
        [InlineData("+45727183919", false)]
        [InlineData("1727183919", false)]
        [InlineData("hest", false)]
        public void IsValidSwedishPhoneNumber_Returns_Expected(string phoneNumber, bool expected)
        {
            var result = PhoneNumberHelper.IsValidSwedishPhoneNumber(phoneNumber);
            Assert.Equal(expected, result);
        }
    }
}
