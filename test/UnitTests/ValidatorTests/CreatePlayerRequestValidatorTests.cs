using Bogus;
using Core.DTOs.Player;
using Core.Validators;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class CreatePlayerRequestValidatorTests 
    {
        [Fact]
        public void CreatePlayerRequestValidator_Should_Return_Ok_For_Valid_Model()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreatePlayerRequestValidator_Should_Return_Failed_For_Empty_Email()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, string.Empty)
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }

        [Theory]
        [InlineData("hej.com")]
        [InlineData("hej@")]
        [InlineData("hej")]
        [InlineData("123123")]
        public void CreatePlayerRequestValidator_Should_Return_Failed_For_Incorrect_Email_Format(string email)
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, email)
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void CreatePlayerRequestValidator_Should_Return_Failed_For_Empty_Name()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, string.Empty)
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void CreatePlayerRequestValidator_Should_Return_Failed_For_Empty_PhoneNumber()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, string.Empty)
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }

        [Theory]
        [InlineData("0700-123123")]
        [InlineData("070012312")]
        [InlineData("hej")]
        public void CreatePlayerRequestValidator_Should_Return_Failed_For_Incorrect_Format_PhoneNumber(string number)
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, string.Empty)
                        .RuleFor(x => x.PhoneNumber, number);

            var validator = new CreatePlayerRequestValidator();
            var result = validator.Validate(faker.Generate());
            Assert.True(!result.IsValid);
        }
    }
}