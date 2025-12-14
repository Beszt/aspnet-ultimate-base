using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using AspNetUltimateBase.Tests.Tokens;

namespace AspNetUltimateBase.Tests.Rest;

public static class Client
{
    readonly static WebApplicationFactory<Program> _application = new();
    readonly static HttpClient _client = _application.CreateClient();

    public static async Task<Response> Send(HttpRequestMessage httpRequest, TokenType tokenType)
    {
        Token token = await TokenFactory.CreateToken(tokenType);
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);

        HttpResponseMessage message = await _client.SendAsync(httpRequest);

        Response response = new()
        {
            Body = await message.Content.ReadAsStringAsync(),
            StatusCode = message.StatusCode
        };

        return response;
    }
    public static async Task<Response> Send(HttpRequestMessage httpRequest)
    {
        HttpResponseMessage message = await _client.SendAsync(httpRequest);

        Response response = new()
        {
            Body = await message.Content.ReadAsStringAsync(),
            StatusCode = message.StatusCode
        };

        return response;
    }
}

