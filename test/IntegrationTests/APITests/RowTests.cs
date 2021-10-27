using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using Core.DatabaseEntities;
using Core.DTOs.Row;
using IntegrationTests.Setup;
using Xunit;

namespace IntegrationTests.APITests
{
    [Collection(nameof(TestFixture))]
    public class RowTests
    {
        private readonly TestFixture _fixture;

        public RowTests(TestFixture fixture) => _fixture = fixture;

        [Fact]
        public async Task CreateRow_Should_Create_Row()
        {
            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(_fixture.Factory.Server.BaseAddress + "v1/rows", null);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            var rowId = JsonSerializer.Deserialize<int>(await postResponse.Content.ReadAsStringAsync());

            var createdRow = await _fixture.GetTestDataAsync<Row>(rowId);

            Assert.Equal(createdRow.RowId, rowId);
        }

        [Fact]
        public async Task UpdateRow_Should_Update_Row_With_New_Values()
        {
            var rowFaker = new Faker<Row>().RuleFor(x => x.Earnings, default(decimal));
            var row = rowFaker.Generate();
            var rowId = await _fixture.InsertTestDataAsync(row);

            var client = _fixture.Factory.CreateClient();

            var updateRowRequest = new UpdateRow { Earnings = 40.50M };

            var content = new StringContent(JsonSerializer.Serialize(updateRowRequest), Encoding.UTF8, "application/json");

            var putResponse = await client.PutAsync(_fixture.Factory.Server.BaseAddress + $"v1/rows/{rowId}", content);

            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);

            var putResponseId = JsonSerializer.Deserialize<int>(await putResponse.Content.ReadAsStringAsync());

            Assert.Equal(rowId, putResponseId);

            var updatedRow = await _fixture.GetTestDataAsync<Row>(putResponseId);

            Assert.Equal(updatedRow.RowId, rowId);
            Assert.Equal(updatedRow.Earnings, updateRowRequest.Earnings);
        }
    }
}
