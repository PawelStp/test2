namespace Games.Api.Models.Authentication
{
    public class AuthenticationResult
    {
        public AuthenticationResult(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
