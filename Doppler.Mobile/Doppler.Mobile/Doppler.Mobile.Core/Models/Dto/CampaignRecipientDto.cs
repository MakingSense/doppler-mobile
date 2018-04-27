using Newtonsoft.Json;
namespace Doppler.Mobile.Core.Models.Dto
{
    public class CampaignRecipientDto
    {
        [JsonProperty(PropertyName = "id", Required = Required.DisallowNull)]
        public int ListId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.DisallowNull)]
        public string Name{ get; set; }

        // Comment: This property is not in the rest api doppler, It will be in a few weeks.
        [JsonProperty(PropertyName = "subscribersCount", Required = Required.DisallowNull)]
        public int SubscribersCount { get; set; }
    }
}
