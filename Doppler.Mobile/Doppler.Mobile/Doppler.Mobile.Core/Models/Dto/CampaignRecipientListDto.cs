using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models.Dto
{
    public class CampaignRecipientListDto
    {
        [JsonProperty(PropertyName = "lists", Required = Required.DisallowNull)]
        public IList<CampaignRecipientDto> Items { get; set; }
    }
}
