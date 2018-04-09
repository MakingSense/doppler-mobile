using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class UserAuthenticationDto
    {
        [JsonProperty(PropertyName = "grant_type")]
        public string GrantType { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
