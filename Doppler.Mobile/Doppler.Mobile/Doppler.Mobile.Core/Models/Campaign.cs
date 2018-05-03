using System;
using System.Collections.Generic;

namespace Doppler.Mobile.Core.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }

        public DateTimeOffset ScheduledDate { get; set; }

        public bool RecipientsRequired { get; set; }

        public bool ContentRequired { get; set; }

        public string Name { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Preheader { get; set; }

        public string ReplyTo { get; set; }

        public bool TextCampaign { get; set; }

        public string Status { get; set; }

        public IList<Link> Links { get; set; }
    }
}
