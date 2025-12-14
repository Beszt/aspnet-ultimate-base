using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FluentAssertions;
using Xunit;
using AspNetUltimateBase.Tests.Rest;
using AspNetUltimateBase.Tests.Tokens;

namespace AspNetUltimateBase.Tests;

[TestCaseOrderer("AspNetUltimateBase.Tests.Orderers.PriorityOrderer", "AspNetUltimateBase.Tests")]
public class ProductEndpointTest
{
    private readonly static string _productBarcode = "5900617041661";
    private readonly string _productJson = "{\"barcode\":" + _productBarcode + ",\"name\":\"GO ON Protein Crisp (Cookies & Cream)\",\"description\":\"Cookies & Cream motherfucker\",\"weight\":50,\"energy\":216,\"protein\":10,\"fat\":8,\"carbohydrates\":26.5,\"sugar\":null,\"salt\":null,\"fiber\":null}";

    [Fact, TestPriority(1)]
    public async Task HttpGet_ShouldReturnNotFoundForAnonymous()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}", UriKind.Relative)
        };

        Response response = await Client.Send(httpRequest);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact, TestPriority(2)]
    public async Task HttpPost_ShouldCreateProduct()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/barcode", UriKind.Relative),
            Content = new StringContent(_productJson, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
        };

        Response response = await Client.Send(httpRequest, TokenType.User);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact, TestPriority(3)]
    public async Task HttpGet_ShouldReturnProduct()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}", UriKind.Relative)
        };

        Response response = await Client.Send(httpRequest, TokenType.User);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Body.Should().Be(_productJson);
    }

    [Fact, TestPriority(4)]
    public async Task HttpDelete_ShouldDeleteProduct()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri($"/barcode/{_productBarcode}", UriKind.Relative)
        };

        Response response = await Client.Send(httpRequest, TokenType.User);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact, TestPriority(5)]
    public async Task HttpGet_ShouldReturnNotFound()
    {
        HttpRequestMessage httpRequest = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/barcode/{_productBarcode}", UriKind.Relative)
        };

        Response response = await Client.Send(httpRequest, TokenType.User);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

