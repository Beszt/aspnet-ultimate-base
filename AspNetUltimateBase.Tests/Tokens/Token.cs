using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AspNetUltimateBase.Tests.Tokens;

public abstract class Token(string _jsonCredentials)
{
    public string Bearer { get; private set; }

    public async Task<Token> InitializeAsync()
    {
        if (string.IsNullOrEmpty(Bearer))
        {
            await using WebApplicationFactory<Program> application = new WebApplicationFactory<Program>();
            using HttpClient client = application.CreateClient();
            string body = _jsonCredentials;

            HttpRequestMessage httpRequest = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("/login", UriKind.Relative),
                Content = new StringContent(body, Encoding.UTF8, new MediaTypeHeaderValue("application/json"))
            };

            HttpResponseMessage response = await client.SendAsync(httpRequest);
            HttpStatusCode statusCode = response.StatusCode;
            if (statusCode == HttpStatusCode.OK)
                Bearer = await response.Content.ReadAsStringAsync();
        }

        return this;
    }
}

