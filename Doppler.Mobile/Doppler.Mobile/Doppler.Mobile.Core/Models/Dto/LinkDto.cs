using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class LinkDto
    {
        [JsonProperty(PropertyName = "href")]
        public string HyperlinkRef { get; set; }

        [JsonProperty(PropertyName = "description", Required = Required.DisallowNull)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "rel", Required = Required.DisallowNull)]
        public string Relation { get; set; }
    }
}
