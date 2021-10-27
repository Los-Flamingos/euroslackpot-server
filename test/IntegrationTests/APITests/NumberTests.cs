using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Bogus;

using Core.DatabaseEntities;
using Core.DTOs.Number;
using Core.Enums;

using IntegrationTests.Setup;
using Xunit;

namespace IntegrationTests.APITests
{
    [Collection(nameof(TestFixture))]
    public class NumberTests
    {
        private readonly TestFixture _fixture;

        public NumberTests(TestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task CreateNumber_Should_Create_New_Number_For_Existing_Row_And_Player()
        {
            var playerFaker = new Faker<Player>()
                              .RuleFor(x => x.Email, f => f.Internet.Email())
                              .RuleFor(x => x.PlayerName, f => f.Name.FullName())
                              .RuleFor(x => x.PhoneNumber, "0700111111");
            var player = playerFaker.Generate();
            var playerId = await _fixture.InsertTestDataAsync(player);

            var rowFaker = new Faker<Row>().RuleFor(x => x.Earnings, f => f.Finance.Amount());
            var row = rowFaker.Generate();
            var rowId = await _fixture.InsertTestDataAsync(row);

            var numberFaker = new Faker<NumberRequest>()
                              .RuleFor(x => x.PlayerId, playerId)
                              .RuleFor(x => x.Week, ISOWeek.GetWeekOfYear(DateTime.UtcNow))
                              .RuleFor(x => x.Type, NumberType.Regular)
                              .RuleFor(x => x.Value, 25);

            var client = _fixture.Factory.CreateClient();

            using var content = new StringContent(JsonSerializer.Serialize(numberFaker.Generate()), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + $"v1/rows/{rowId}/numbers"), content);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreateNumber_Should_Return_BadRequest_For_Missing_Week()
        {
            var numberFaker = new Faker<NumberRequest>()
                              .RuleFor(x => x.PlayerId, 1)
                              .RuleFor(x => x.Type, NumberType.Regular)
                              .RuleFor(x => x.Value, 25);

            var client = _fixture.Factory.CreateClient();

            using var content = new StringContent(JsonSerializer.Serialize(numberFaker.Generate()), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/rows/1/numbers"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreateNumber_Should_Return_BadRequest_For_Missing_Value()
        {
            var numberFaker = new Faker<NumberRequest>()
                              .RuleFor(x => x.PlayerId, 1)
                              .RuleFor(x => x.Type, NumberType.Regular)
                              .RuleFor(x => x.Week, 25);

            var client = _fixture.Factory.CreateClient();

            using var content = new StringContent(JsonSerializer.Serialize(numberFaker.Generate()), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/rows/1/numbers"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreateNumber_Should_Return_BadRequest_For_Missing_PlayerId()
        {
            var numberFaker = new Faker<NumberRequest>()
                              .RuleFor(x => x.Value, 1)
                              .RuleFor(x => x.Type, NumberType.Regular)
                              .RuleFor(x => x.Week, 25);

            var client = _fixture.Factory.CreateClient();

            using var content = new StringContent(JsonSerializer.Serialize(numberFaker.Generate()), Encoding.UTF8, "application/json");

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/rows/1/numbers"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }
    }
}
