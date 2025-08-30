using Xunit;
using System.Net;
using Infrastructure.ExternalAPIs;
using DotNetEnv;
public class NationalityServiceTests
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
    public async Task GetNationalityAsync_EmptyName_ReturnsExpectedResult()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"count\":10, \"name\":\"\", \"country\":[{\"country_id\":\"SD\",\"probability\":0.3487012644088011},{\"country_id\":\"KW\",\"probability\":0.17784708383786518},{\"country_id\":\"YE\",\"probability\":0.14168719471228072},{\"country_id\":\"EG\",\"probability\":0.0574958135962964}]}")
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_NATIONALIZE_API"))
        };
        var service = new NationalityService(httpClient);
        var result = await service.GetNationalityAsync("");

        Assert.Equal("", result.Name);
        Assert.Equal(4, result.Country.Count);
        Assert.Equal("SD", result.Country[0].CountryId);
        Assert.Equal(0.3487012644088011, result.Country[0].Probability);
    }

    [Fact]
    public async Task GetNationalityAsync_MalformedJson_ThrowsException()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ invalid json }")
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_NATIONALIZE_API"))

        };
        var service = new NationalityService(httpClient);

        await Assert.ThrowsAsync<System.Text.Json.JsonException>(() => service.GetNationalityAsync("cram"));
    }

    [Fact]
    public async Task GetNationalityAsync_ApiFailure_ThrowsException()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_NATIONALIZE_API"))
        };
        var service = new NationalityService(httpClient);

        await Assert.ThrowsAsync<HttpRequestException>(() => service.GetNationalityAsync("cram"));
    }



    [Fact]
    public async Task GetNationalityAsync_ReturnsExpectedResult()
    {
        var mockHandler = new MockHttpMessageHandler
        {
            HandlerFunc = (_, _) => Task.FromResult(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"name\": \"cram\", \"country\": [ { \"country_id\": \"PH\", \"probability\": 0.9 }, { \"country_id\": \"US\", \"probability\": 0.8 } ] }")
            })
        };

        var httpClient = new HttpClient(mockHandler)
        {
            BaseAddress = new Uri(TestEnv.Get("TEST_NATIONALIZE_API"))

        };
        var service = new NationalityService(httpClient);

        var result = await service.GetNationalityAsync("cram");

        Assert.Equal("cram", result.Name);
        Assert.Equal(2, result.Country.Count);
        Assert.Equal("PH", result.Country[0].CountryId);
        Assert.Equal(0.9, result.Country[0].Probability);
    }
}
