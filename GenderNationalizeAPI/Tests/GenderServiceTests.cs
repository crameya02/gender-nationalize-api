using Xunit;
using System.Text.Json;
using System.Net;
using Infrastructure.ExternalAPIs;
using DotNetEnv;
public class GenderServiceTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        public Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>? HandlerFunc { get; set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HandlerFunc == null)
                throw new InvalidOperationException("HandlerFunc must be set before use.");

            return HandlerFunc(request, cancellationToken);
        }
    }

    [Fact]
    public async Task GetGenderAsync_InvalidJson_ThrowsException()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("not a json")
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_GENDERIZE_API"))
        };


        var service = new GenderService(httpClient);


        await Assert.ThrowsAsync<JsonException>(() => service.GetGenderAsync("cram"));
    }
    [Fact]
    public async Task GetGenderAsync_EmptyName_ReturnsDefaultResult()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"name\":\"\",\"gender\":null,\"probability\":0.0,\"count\":0}")
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_GENDERIZE_API"))
        };
        var service = new GenderService(httpClient);

        var result = await service.GetGenderAsync("");

        Assert.Equal("", result.Name);
        Assert.Null(result.Gender);
    }

    [Fact]
    public async Task GetGenderAsync_ReturnsExpectedResult()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (request, cancellationToken) =>
                Task.FromResult(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"name\":\"cram\",\"gender\":\"male\",\"probability\":0.99,\"count\":1000}")
                })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_GENDERIZE_API"))
        };
        var service = new GenderService(httpClient);

        var result = await service.GetGenderAsync("cram");

        Assert.Equal("cram", result.Name);
        Assert.Equal("male", result.Gender);
    }

}
