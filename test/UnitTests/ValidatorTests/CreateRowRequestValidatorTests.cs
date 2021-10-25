using Bogus;
using Core.DTOs.Row;
using Core.Validators;

using Xunit;

namespace UnitTests.ValidatorTests
{
    public class CreateRowRequestValidatorTests
    {
        [Fact]
        public void CreateRowRequestValidator_Should_Succeed_For_Valid_Model()
        {
            var faker = new Faker<CreateRowRequest>()
                .RuleFor(x => x.Week, 1);

            var validator = new CreateRowRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateRowRequestValidator_Should_Fail_For_Default_Value_For_Week()
        {
            var faker = new Faker<CreateRowRequest>()
                .RuleFor(x => x.Week, 0);

            var validator = new CreateRowRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }
    }
}
