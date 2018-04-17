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

        private int? _currentCampaignPage { get; set; }

        public int CurrentCampaignPageNumber 
        {
            get => _currentCampaignPage ?? 0;
        }

        /// <inheritdoc />
        public async Task<IList<Campaign>> GetCampaignsAsync()
        {
            var firstPage = 1;
            var campaignList = await FetchCampaignsAsync(firstPage);

            return campaignList;
        }

        /// <inheritdoc />
        public async Task<IList<Campaign>> GetMoreCampaignsAsync()
        {
            var nextCampaignListPage = ( _currentCampaignPage ?? 0 ) + 1;
            var campaignList = await FetchCampaignsAsync(nextCampaignListPage);
            return campaignList;
        }

        private async Task<IList<Campaign>> FetchCampaignsAsync(int pageNumber)
        {
            var accountName = GetAccountNameLoggedIn();
            if (string.IsNullOrEmpty(accountName))
                return new List<Campaign>();
            var fetchCampaigns = await _dopplerApi.GetCampaignsAsync(accountName, pageNumber);
            _currentCampaignPage = pageNumber;
            if (!fetchCampaigns.IsSuccessResult)
                return new List<Campaign>();

            var campaignList = fetchCampaigns.SuccessValue.Items.Select(Mapper.Mapper.ToCampaign).ToList();
            return campaignList;
        }

        private string GetAccountNameLoggedIn()
        {
            var currentAccountName = _localSettings.AccountNameLoggedIn;
            return currentAccountName;
        }
    }
}
