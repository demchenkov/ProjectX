using Newtonsoft.Json;

namespace Web.ViewModels.Account.Responses
{
    public class LoginResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}