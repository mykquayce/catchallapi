using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace CatchAllApi.Tests.Fixtures
{
	public class HttpClientFixture : IDisposable
	{
		private readonly TestServer _testServer;

		public HttpClientFixture()
		{
			var builder = new WebHostBuilder()
				.UseStartup<WebApplication.Startup>();

			_testServer = new TestServer(builder);
			HttpClient = _testServer.CreateClient();
		}

		public HttpClient HttpClient { get; }

		public void Dispose()
		{
			HttpClient?.Dispose();
			_testServer?.Dispose();
		}
	}
}
