namespace APICatalogo.DTOs.Auth
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpireRefreshToken { get; set; }

        public TokenResponse()
        {
        }

        public TokenResponse(string token, string refreshToken, DateTime expireRefreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
            ExpireRefreshToken = expireRefreshToken;
        }
    }
}
