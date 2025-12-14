using System.Net;

namespace AspNetUltimateBase.Tests.Rest;

public class Response()
{
    public string Body {get; init; }
    public HttpStatusCode StatusCode {get; init; }
}

