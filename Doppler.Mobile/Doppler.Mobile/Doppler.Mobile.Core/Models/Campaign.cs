using System;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Models
{
    public class Campaign
    {
        [JsonProperty(PropertyName = "campaignId", Required = Required.DisallowNull)]
        public string CampaignId { get; set; }

        [JsonProperty(PropertyName = "scheduledDate", Required = Required.DisallowNull)]
        public DateTimeOffset ScheduledDate { get; set; }

        [JsonProperty(PropertyName = "recipientsRequired", Required = Required.DisallowNull)]
        public bool RecipientsRequired { get; set; }

        [JsonProperty(Required = Required.DisallowNull, PropertyName = "contentRequired")]
        public bool ContentRequired { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.DisallowNull)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "fromName", Required = Required.DisallowNull)]
        public string FromName { get; set; }

        [JsonProperty(PropertyName = "fromEmail", Required = Required.DisallowNull)]
        public string FromEmail { get; set; }

        [JsonProperty(PropertyName = "subject", Required = Required.DisallowNull)]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "preheader", Required = Required.DisallowNull)]
        public string Preheader { get; set; }

        [JsonProperty(PropertyName = "replyTo", Required = Required.DisallowNull)]
        public string ReplyTo { get; set; }

        [JsonProperty(PropertyName = "textCampaign", Required = Required.DisallowNull)]
        public bool TextCampaign { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.DisallowNull)]
        public string Status { get; set; }
    }
}
