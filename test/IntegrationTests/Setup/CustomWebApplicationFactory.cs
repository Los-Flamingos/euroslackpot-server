using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Setup
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
    }
}
