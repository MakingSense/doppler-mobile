using System;
using System.Collections.Generic;
using Doppler.Mobile.Core.Models.Dto;

namespace Doppler.Mobile.Test.Mocks
{
    public static class Mocks
    {
        public static PageDto<CampaignDto> GetPageCampaignDto()
        {
            var campaign1 = GetCampaignDto(1);
            var campaign2 = GetCampaignDto(2);

            return new PageDto<CampaignDto>
            {
                Items = new CampaignDto[] { campaign1, campaign2 },
                PageSize = 2,
                CurrentPage = 1,
                PagesCount = 2,
                ItemsCount = 4
            };
        }

        public static CampaignDto GetCampaignDto(int identifier)
        {
            var linkDto1 = GetLinkDto(1);
            var linkDto2 = GetLinkDto(2);

            return new CampaignDto
            {
                CampaignId = identifier,
                ScheduledDate = new DateTimeOffset(),
                RecipientsRequired = false,
                ContentRequired = true,
                Name = $"Name-{identifier}",
                FromName = $"FromName-{identifier}",
                FromEmail = $"FromEmail-{identifier}",
                Subject = $"Subject-{identifier}",
                Preheader = $"Preheader-{identifier}",
                ReplyTo = $"ReplyTo-{identifier}",
                TextCampaign = true,
                Status = $"Status-{identifier}",
                Links = new LinkDto[] { linkDto1, linkDto2 }
            };
        }

        public static LinkDto GetLinkDto(int identifier)
        {
            return new LinkDto
            {
                HyperlinkRef = $"Hyper Reft {identifier}",
                Relation = $"Relation {identifier}",
                Description = $"Description {identifier}"
            };
        }

        public static UserAuthenticationResponseDto GetUserAuthenticationResponseDto()
        {
            return new UserAuthenticationResponseDto
            {
                AccessToken = "AccessToken",
                AccountId = 1,
                Username = "userTest",
                IssuedAt = "testDate",
                ExpirationDate = "Expiration Date"
            };
        }

        public static CampaignRecipientDto GetCampaignRecipientsDto(int identifier)
        {
            return new CampaignRecipientDto
            {
                Name = "Campaign Recipient Name",
                ListId = identifier,
                SubscribersCount = 100
            };
        }

        public static CampaignRecipientListDto GetCampaignRecipientListDto()
        {
            var campaignRecipients1 = GetCampaignRecipientsDto(1);
            var campaignRecipients2 = GetCampaignRecipientsDto(2);

            return new CampaignRecipientListDto
            {
                Items = new CampaignRecipientDto[] { campaignRecipients1, campaignRecipients2 }
            };
        }
    }
}
