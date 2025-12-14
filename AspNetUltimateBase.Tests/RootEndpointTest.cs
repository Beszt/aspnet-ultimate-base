using System.Net;
using FluentAssertions;
using Xunit;
using AspNetUltimateBase.Tests.Rest;

namespace AspNetUltimateBase.Tests;

public class RootEndpointTest
{
    [Fact]
    public async Task HttpGet_ShouldReturnStatusCodeNotFound()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/", UriKind.Relative)
        };

        Response response = await Client.Send(httpRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

