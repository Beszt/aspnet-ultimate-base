using FluentAssertions;
using Xunit;
using AspNetUltimateBase.Tests.Tokens;

namespace AspNetUltimateBase.Tests;

public class LoginEndpointTest
{
    readonly string _bearerHeader = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

    [Fact]
    public async Task HttpPost_GetAdminToken()
    {
        Token token = await TokenFactory.CreateToken(TokenType.Admin);
    
        token.Bearer.Should().Contain(_bearerHeader);
    }

    [Fact]
    public async Task HttpPost_GetUserToken()
    {
        Token token = await TokenFactory.CreateToken(TokenType.User);
    
        token.Bearer.Should().Contain(_bearerHeader);
    }
}

