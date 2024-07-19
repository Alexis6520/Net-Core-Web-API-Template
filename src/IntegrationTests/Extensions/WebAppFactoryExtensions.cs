using IntegrationTests.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace IntegrationTests.Extensions
{
    public static class WebAppFactoryExtensions
    {
        public static HttpClient CreateAuthenticatedClient<T>(this WebApplicationFactory<T> factory) where T : class
        {
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(opts => opts.DefaultAuthenticateScheme = "TestScheme")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", opts => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
            return client;
        }
    }
}
