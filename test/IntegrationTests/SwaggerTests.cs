using System.Net;
using System.Threading.Tasks;
using API;
using IntegrationTests.Setup;
using Xunit;

namespace IntegrationTests
{
    [Collection(nameof(TestFixture))]
    public class SwaggerTests
    {
        private readonly TestFixture _fixture;

        public SwaggerTests(TestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task SwaggerUI_Should_Render_SwaggerUI_With_200OK_StatusCode()
        {
            var client = _fixture.CreateClient();

            var httpResponse = await client.GetAsync("/swagger");

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [Fact]
        public async Task SwaggerGen_Should_Render_SwaggerGen_With_200OK_StatusCode()
        {
            var client = _fixture.CreateClient();

            var httpResponse = await client.GetAsync("/swagger/v1/swagger.json");

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        }
    }
}
