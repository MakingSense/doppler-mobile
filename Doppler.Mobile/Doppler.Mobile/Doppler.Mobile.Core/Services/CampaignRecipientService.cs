using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Settings;

namespace Doppler.Mobile.Core.Services
{
    /// <inheritdoc />
    public class CampaignRecipientService : ICampaignRecipientService
    {
        private readonly ILocalSettings _localSettings;
        private readonly IDopplerAPI _dopplerApi;

        public CampaignRecipientService(ILocalSettings localSettings, IDopplerAPI dopplerApi)
        {
            _localSettings = localSettings;
            _dopplerApi = dopplerApi;
        }

        /// <inheritdoc />
        public async Task<Result<IList<CampaignRecipient>, string>> FetchCampaignRecipientsAsync(int campaignId)
        {
            var accountName = GetAccountNameLoggedIn();
            if (string.IsNullOrEmpty(accountName))
                return new Result<IList<CampaignRecipient>, string>(errorValue: CoreResources.NotUserLoggedIn);

            var fetchCampaignRecipients = await _dopplerApi.GetCampaignRecipientsAsync(accountName, campaignId);
            if (!fetchCampaignRecipients.IsSuccessResult)
                return new Result<IList<CampaignRecipient>, string>(errorValue: fetchCampaignRecipients.ErrorValue);

            var campaignRecipients = fetchCampaignRecipients.SuccessValue.Items.Select(Mapper.Mapper.ToCampaignRecipient).ToList();
            return new Result<IList<CampaignRecipient>, string>(successValue: campaignRecipients);
        }

        private string GetAccountNameLoggedIn()
        {
            var currentAccountName = _localSettings.AccountNameLoggedIn;
            return currentAccountName;
        }
    }
}
