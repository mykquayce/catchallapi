using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CatchAllApi.Tests
{
	public class UnitTest1 : IClassFixture<Fixtures.HttpClientFixture>
	{
		private readonly HttpClient _httpClient;

		public UnitTest1(Fixtures.HttpClientFixture fixture)
		{
			_httpClient = fixture.HttpClient;
		}

		[Theory]
		[InlineData("get", "")]
		[InlineData("get", "/")]
		[InlineData("get", "/arst")]
		[InlineData("get", "/arst/arst")]
		[InlineData("get", "/arst/arst?arst=arst")]
		[InlineData("post", "")]
		[InlineData("post", "/")]
		[InlineData("post", "/arst")]
		[InlineData("post", "/arst/arst")]
		[InlineData("post", "/arst/arst?arst=arst")]
		public async Task Test1(string methodString, string uriString)
		{
			var method = new HttpMethod(methodString);

			using var requestMessage = new HttpRequestMessage(method, uriString);

			using var responseMessage = await _httpClient.SendAsync(requestMessage);

			Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);

			var contents = await responseMessage.Content!.ReadAsStringAsync();

			Assert.Equal("Hello World", contents);
		}
	}
}
