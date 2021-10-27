using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using Core.DatabaseEntities;
using Core.DTOs.Player;
using IntegrationTests.Setup;
using Xunit;

namespace IntegrationTests.APITests
{
    [Collection(nameof(TestFixture))]
    public class PlayerTests
    {
        private readonly TestFixture _fixture;

        public PlayerTests(TestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task GetPlayerById_Should_Get_Player_For_Id()
        {
            var faker = new Faker<Player>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.PlayerName, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var player = faker.Generate();

            var id = await _fixture.InsertTestDataAsync(player);

            var client = _fixture.Factory.CreateClient();

            var getResponse = await client.GetAsync(new Uri(_fixture.Factory.Server.BaseAddress + $"v1/players/{id}"));

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var getPlayerByIdResponse = JsonSerializer.Deserialize<GetPlayerByIdResponse>(await getResponse.Content.ReadAsStringAsync());

            Assert.NotNull(getPlayerByIdResponse);
            Assert.Equal(id, getPlayerByIdResponse.Id);
            Assert.Equal(player.Email, getPlayerByIdResponse.Email);
            Assert.Equal(player.PlayerName, getPlayerByIdResponse.Name);
            Assert.Equal(player.PhoneNumber, getPlayerByIdResponse.PhoneNumber);
        }

        [Fact]
        public async Task CreatePlayer_Should_Create_Player()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var player = faker.Generate();

            using var content = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/players"), content);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            var createdPlayerId = JsonSerializer.Deserialize<int>(await postResponse.Content.ReadAsStringAsync());

            var getResponse = await client.GetAsync(new Uri(_fixture.Factory.Server.BaseAddress + $"v1/players/{createdPlayerId}"));

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var getPlayerByIdResponse = JsonSerializer.Deserialize<GetPlayerByIdResponse>(await getResponse.Content.ReadAsStringAsync());

            Assert.NotNull(getResponse);
            Assert.Equal(player.Email, getPlayerByIdResponse.Email);
            Assert.Equal(player.Name, getPlayerByIdResponse.Name);
            Assert.Equal(player.PhoneNumber, getPlayerByIdResponse.PhoneNumber);
        }

        [Fact]
        public async Task CreatePlayer_Should_Return_BadRequest_For_Invalid_Email()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, "randomstring")
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "0700111111");

            var player = faker.Generate();

            using var content = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/players"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreatePlayer_Should_Return_BadRequest_For_Invalid_PhoneNumber()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, f => f.Name.FullName())
                        .RuleFor(x => x.PhoneNumber, "asd");

            var player = faker.Generate();

            using var content = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/players"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }

        [Fact]
        public async Task CreatePlayer_Should_Return_BadRequest_For_Missing_Name()
        {
            var faker = new Faker<CreatePlayerRequest>()
                        .RuleFor(x => x.Email, f => f.Internet.Email())
                        .RuleFor(x => x.Name, f => string.Empty)
                        .RuleFor(x => x.PhoneNumber, "asd");

            var player = faker.Generate();

            using var content = new StringContent(JsonSerializer.Serialize(player), Encoding.UTF8, "application/json");

            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(new Uri(_fixture.Factory.Server.BaseAddress + "v1/players"), content);

            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
        }
    }
}
