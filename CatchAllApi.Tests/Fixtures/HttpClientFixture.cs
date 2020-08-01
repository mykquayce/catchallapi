using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace CatchAllApi.Tests.Fixtures
{
	public class HttpClientFixture : WebApplicationFactory<CatchAllApi.WebApplication.Startup>
	{
		private readonly static WebApplicationFactoryClientOptions _options = new WebApplicationFactoryClientOptions
		{
			AllowAutoRedirect = false,
		};

		private HttpClient? _httpClient;

		public HttpClient HttpClient => _httpClient ??= base.CreateClient(_options);
	}
}
