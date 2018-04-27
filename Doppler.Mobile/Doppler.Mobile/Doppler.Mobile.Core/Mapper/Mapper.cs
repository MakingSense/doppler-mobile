using System.Linq;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Models.Dto;

namespace Doppler.Mobile.Core.Mapper
{
    public static class Mapper
    {
        public static Campaign ToCampaign(CampaignDto dto)
        {
            return new Campaign
            {
                CampaignId = dto.CampaignId,
                ScheduledDate = dto.ScheduledDate,
                RecipientsRequired = dto.RecipientsRequired,
                ContentRequired = dto.ContentRequired,
                Name = dto.Name,
                FromName = dto.FromName,
                FromEmail = dto.FromEmail,
                Subject = dto.Subject,
                Preheader = dto.Preheader,
                ReplyTo = dto.ReplyTo,
                TextCampaign = dto.TextCampaign,
                Status = dto.Status,
                Links = dto.Links.Select(ToLink).ToList()
            };
        }

        public static Page<Campaign> ToPageCampaign(PageDto<CampaignDto> dto)
        {
            return new Page<Campaign>
            {
                Items = dto.Items.Select(ToCampaign).ToList(),
                PageSize = dto.PageSize,
                ItemsCount = dto.ItemsCount,
                CurrentPage = dto.CurrentPage,
                PagesCount = dto.PagesCount
            };
        }

        public static CampaignRecipient ToCampaignRecipient(CampaignRecipientDto dto)
        {
            return new CampaignRecipient
            {
                Name = dto.Name,
                ListId = dto.ListId,
                SubscribersCount = dto.SubscribersCount
            };
        }

        public static Link ToLink(LinkDto dto)
        {
            return new Link
            {
                HyperlinkRef = dto.HyperlinkRef,
                Description = dto.Description,
                Relation = dto.Relation
            };
        }
    }
}
