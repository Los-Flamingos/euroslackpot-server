using System.Data.SqlClient;
using System.Threading.Tasks;
using API;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Xunit;

namespace IntegrationTests.Setup
{
    [CollectionDefinition(nameof(TestFixture))]
    public class TestFixtureCollection : ICollectionFixture<TestFixture> { }

    public class TestFixture : IAsyncLifetime
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Checkpoint _checkPoint;
        private readonly IConfiguration _configuration;

        public TestFixture()
        {
            _factory = new EuroslackpotTestApplicationFactory();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _checkPoint = new Checkpoint();
        }

        public class EuroslackpotTestApplicationFactory : WebApplicationFactory<Startup> { }

        public Task InitializeAsync() => _checkPoint.Reset(_configuration["Database:ConnectionString"]);

        public WebApplicationFactory<Startup> Factory => _factory;

        public async Task<int> InsertTestDataAsync<T>(T entity) where T : class
        {
            await using var connection = new SqlConnection(_configuration["Database:ConnectionString"]);
            await connection.OpenAsync();
            return await connection.InsertAsync<T>(entity);
        }

        public async Task<T> GetTestDataAsync<T>(int rowId) where T : class
        {
            await using var connection = new SqlConnection(_configuration["Database:ConnectionString"]);
            await connection.OpenAsync();
            return await connection.GetAsync<T>(rowId);
        }

        public Task DisposeAsync()
        {
            _factory?.Dispose();
            return Task.CompletedTask;
        }
    }
}