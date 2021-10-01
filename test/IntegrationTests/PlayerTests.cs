using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using API;
using Core.DTOs.Player;
using IntegrationTests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests
{
    public class PlayerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly ITestOutputHelper _testOutputHelper;

        public PlayerTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetAll_Should_Return_List_Of_Players()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync(new Uri($"{client.BaseAddress}/v1/players"));

            if (!result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                _testOutputHelper.WriteLine(message);
                return;
            }

            // assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task GetAll_Should_Return_Player_For_Id()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync(new Uri($"{client.BaseAddress}/v1/players"));

            Assert.True(result.IsSuccessStatusCode);

            var message = await result.Content.ReadAsStringAsync();

            var player = JsonSerializer.Deserialize<List<GetAllPlayersResponse>>(message);
            var firstPlayer = player.First();
            var playerByIdResponse = await client.GetAsync(new Uri($"{client.BaseAddress}/v1/players/{firstPlayer.Id}"));
            
            Assert.True(playerByIdResponse.IsSuccessStatusCode);

            message = await playerByIdResponse.Content.ReadAsStringAsync();

            var playerById = JsonSerializer.Deserialize<GetPlayerByIdResponse>(message);

            Assert.NotNull(playerById);
            Assert.Equal(firstPlayer.Id, playerById.Id);
            Assert.Equal(firstPlayer.Name, playerById.Name);
        }
    }
}