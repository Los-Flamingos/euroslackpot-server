using Core.DTOs.Number;
using Core.Enums;
using Core.Validators;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class CreateNumberRequestValidatorTests
    {
        [Fact]
        public void CreateNumberRequestValidator_Should_Return_Ok_For_Valid_Model()
        {
            var request = new CreateNumberRequest
            {
                RowId = 1,
                NumberRequest = new NumberRequest
                {
                    Week = 1,
                    PlayerId = 1,
                    Value = 1,
                    Type = NumberType.Bonus,
                },
            };

            var validator = new CreateNumberRequestValidator();
            var result = validator.Validate(request);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateNumberRequestValidator_Should_Fail_For_Default_Week()
        {
            var request = new CreateNumberRequest
            {
                RowId = 1,
                NumberRequest = new NumberRequest
                {
                    PlayerId = 1,
                    Value = 1,
                    Type = NumberType.Bonus,
                },
            };

            var validator = new CreateNumberRequestValidator();
            var result = validator.Validate(request);
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void CreateNumberRequestValidator_Should_Fail_For_Default_PlayerId()
        {
            var request = new CreateNumberRequest
            {
                RowId = 1,
                NumberRequest = new NumberRequest
                {
                    Week = 1,
                    Value = 1,
                    Type = NumberType.Bonus,
                },
            };

            var validator = new CreateNumberRequestValidator();
            var result = validator.Validate(request);
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void CreateNumberRequestValidator_Should_Fail_For_Default_Value()
        {
            var request = new CreateNumberRequest
            {
                RowId = 1,
                NumberRequest = new NumberRequest
                {
                    Week = 1,
                    PlayerId = 1,
                    Type = NumberType.Bonus,
                },
            };

            var validator = new CreateNumberRequestValidator();
            var result = validator.Validate(request);
            Assert.True(!result.IsValid);
        }

        [Fact]
        public void CreateNumberRequestValidator_Should_Fail_For_Default_RowId()
        {
            var request = new CreateNumberRequest
            {
                NumberRequest = new NumberRequest
                {
                    Week = 1,
                    PlayerId = 1,
                    Value = 1,
                    Type = NumberType.Bonus,
                },
            };

            var validator = new CreateNumberRequestValidator();
            var result = validator.Validate(request);
            Assert.True(!result.IsValid);
        }
    }
}