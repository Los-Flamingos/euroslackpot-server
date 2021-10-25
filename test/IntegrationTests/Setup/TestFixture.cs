using System;
using System.Net.Http;
using System.Threading.Tasks;
using API;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly Checkpoint _checkPoint;
        private readonly IConfiguration _configuration;

        public TestFixture()
        {
            _factory = new EuroslackpotTestApplicationFactory();
            _configuration = _factory.Services.GetRequiredService<IConfiguration>();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            _checkPoint = new Checkpoint();
        }

        public class EuroslackpotTestApplicationFactory : WebApplicationFactory<Startup> { }

        public Task InitializeAsync() => _checkPoint.Reset(_configuration["Database:ConnectionString"]);

        public HttpClient CreateClient() => _factory.CreateClient();

        public Task DisposeAsync()
        {
            _factory?.Dispose();
            return Task.CompletedTask;
        }
    }
}