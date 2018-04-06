using System;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class DopplerErrorDto
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty(PropertyName = "detail")]
        public string Detail{ get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
