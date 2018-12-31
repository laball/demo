using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace CoreApp
{
    public interface ITokenService
    {
        [Multipart]
        [Post("/connect/token")]
        Task<TokenResult> GetToken(string grant_type, string username, string password, string client_id, string client_secret);
    }

    public class TokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }

        [JsonIgnore]
        public bool Success
        {
            get
            {
                return !string.IsNullOrEmpty(Error);
            }
        }

    }



}

