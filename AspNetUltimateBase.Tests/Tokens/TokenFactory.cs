namespace AspNetUltimateBase.Tests.Tokens;

public static class TokenFactory
{
    public async static Task<Token> CreateToken(TokenType type)
    {
        return type switch
        {
            TokenType.Admin => await new AdminToken().InitializeAsync(),
            TokenType.User => await new UserToken().InitializeAsync(),
            _ => throw new Exception($"Token type: {type} not exists")
        };
    }
}

