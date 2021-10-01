using System;
using System.Net;
using System.Threading.Tasks;
using API;
using IntegrationTests.Setup;
using Xunit;

namespace IntegrationTests
{
    public class SwaggerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public SwaggerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SwaggerUI_Should_Render_SwaggerUI_With_200OK_StatusCode()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var httpResponse = await client.GetAsync(new Uri($"{client.BaseAddress}/swagger"));

            // assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task SwaggerGen_Should_Render_SwaggerGen_With_200OK_StatusCode()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var httpResponse = await client.GetAsync(new Uri($"{client.BaseAddress}/swagger/v1/swagger.json"));

            // assert
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
