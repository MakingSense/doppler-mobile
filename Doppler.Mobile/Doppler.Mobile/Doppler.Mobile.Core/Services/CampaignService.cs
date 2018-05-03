using System.Linq;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Settings;

namespace Doppler.Mobile.Core.Services
{
    /// <inheritdoc />
    public class CampaignService : ICampaignService
    {
        private readonly ILocalSettings _localSettings;
        private readonly IDopplerAPI _dopplerApi;

        public CampaignService(ILocalSettings localSettings, IDopplerAPI dopplerApi)
        {
            _localSettings = localSettings;
            _dopplerApi = dopplerApi;
        }

        /// <inheritdoc />
        public async Task<Result<Page<Campaign>, string>> FetchCampaignsAsync(int pageNumber, string campaignType)
        {
            var accountName = GetAccountNameLoggedIn();
            if (string.IsNullOrEmpty(accountName))
                return new Result<Page<Campaign>, string>(errorValue:CoreResources.NotUserLoggedIn);

            var fetchCampaigns = await _dopplerApi.GetCampaignsAsync(accountName, pageNumber, campaignType);
            if (!fetchCampaigns.IsSuccessResult)
                return new Result<Page<Campaign>, string>(errorValue: fetchCampaigns.ErrorValue);

            var newPage = Mapper.Mapper.ToPageCampaign(fetchCampaigns.SuccessValue);
            return new Result<Page<Campaign>, string>(successValue: newPage);
        }

        public Result<string, string> GetCampaignImagePreview(Campaign campaign)
        {
            var linkPreview = campaign.Links.FirstOrDefault(l => l.Relation.Contains("get-campaign-preview"));

            if (linkPreview == null)
                return new Result<string, string>(errorValue: CoreResources.NotPreview);

            return new Result<string, string>(successValue: linkPreview.HyperlinkRef);
        }

        public async Task<Result<string, string>> GetCampaignHtmlPreviewAsync(Campaign campaign)
        {
            var accountName = GetAccountNameLoggedIn();
            if (string.IsNullOrEmpty(accountName))
                return new Result<string, string>(errorValue: CoreResources.NotUserLoggedIn);

            var htmlPreviewResult = await _dopplerApi.GetCampaignHtmlPreviewAsync(accountName, campaign.CampaignId);
            if (!htmlPreviewResult.IsSuccessResult)
                return new Result<string, string>(errorValue: htmlPreviewResult.ErrorValue);

            return new Result<string, string>(successValue: htmlPreviewResult.SuccessValue);
        }

        private string GetAccountNameLoggedIn()
        {
            var currentAccountName = _localSettings.AccountNameLoggedIn;
            return currentAccountName;
        }
    }
}
