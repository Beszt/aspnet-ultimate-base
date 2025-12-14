namespace AspNetUltimateBase.Tests.Tokens;

public class UserToken : Token
{
    public UserToken() : base("{\"login\": \"user\",\"password\": \"12345\"}") { }
}

