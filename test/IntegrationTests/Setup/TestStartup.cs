using API;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Setup
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}