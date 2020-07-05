using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace CatchAllApi.Tests.Fixtures
{
	public class HttpClientFixture : IDisposable
	{
		public HttpClientFixture()
		{
			var builder = new WebHostBuilder()
				.UseStartup<WebApplication.Startup>();

			TestServer = new TestServer(builder);
			HttpClient = TestServer.CreateClient();
		}

		public HttpClient HttpClient { get; }
		public TestServer TestServer { get; }

		public void Dispose()
		{
			HttpClient?.Dispose();
			TestServer?.Dispose();
		}
	}
}
