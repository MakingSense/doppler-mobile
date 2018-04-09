using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class UserAuthenticationResponseDto
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public int AccountId { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "issued_at")]
        public string IssuedAt { get; set; }

        [JsonProperty(PropertyName = "expiration_date")]
        public string ExpirationDate { get; set; }
    }
}
