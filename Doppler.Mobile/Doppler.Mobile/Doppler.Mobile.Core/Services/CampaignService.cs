using System.Collections.Generic;
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
        public async Task<Result<Page<Campaign>, string>> FetchCampaignsAsync(int pageNumber)
        {
            var accountName = GetAccountNameLoggedIn();
            if (string.IsNullOrEmpty(accountName))
                return new Result<Page<Campaign>, string>(errorValue:CoreResources.NotUserLoggedIn);

            var fetchCampaigns = await _dopplerApi.GetCampaignsAsync(accountName, pageNumber);
            if (!fetchCampaigns.IsSuccessResult)
                return new Result<Page<Campaign>, string>(errorValue: fetchCampaigns.ErrorValue);

            var newPage = Mapper.Mapper.ToPageCampaign(fetchCampaigns.SuccessValue);
            return new Result<Page<Campaign>, string>(successValue: newPage);
        }

        private string GetAccountNameLoggedIn()
        {
            var currentAccountName = _localSettings.AccountNameLoggedIn;
            return currentAccountName;
        }
    }
}
