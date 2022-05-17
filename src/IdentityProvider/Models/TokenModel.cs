namespace IdentityProvider.Models
{
    public class TokenModel
    {
        public string Access_token { get; set; }
        public string Expires_in { get; set; }
        public string Refresh_token { get; set; }
        public string Refresh_token_expiryTime { get; set; }
    }
}
