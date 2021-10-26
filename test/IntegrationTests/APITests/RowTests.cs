using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using Core.DatabaseEntities;
using Core.DTOs.Player;
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
        public async Task CreateRow_Should_Create_Row_For_Valid_Model()
        {
            var faker = new Faker<CreateRowRequest>()
                        .RuleFor(x => x.Earnings, f => f.Finance.Amount());

            var row = faker.Generate();

            var content = new StringContent(JsonSerializer.Serialize(row), Encoding.UTF8, "application/json");

            var client = _fixture.Factory.CreateClient();

            var postResponse = await client.PostAsync(_fixture.Factory.Server.BaseAddress + "v1/rows", content);

            Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

            var rowId = JsonSerializer.Deserialize<int>(await postResponse.Content.ReadAsStringAsync());

            var createdRow = await _fixture.GetTestDataAsync<Row>(rowId);

            Assert.Equal(createdRow.RowId, rowId);
            Assert.Equal(createdRow.Earnings, row.Earnings);
        }
    }
}
