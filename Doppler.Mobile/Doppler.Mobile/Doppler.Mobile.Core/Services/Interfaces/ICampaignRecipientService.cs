using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Networking;

namespace Doppler.Mobile.Core.Services
{
    /// <summary> Handles the campaign recipients information </summary>
    public interface ICampaignRecipientService
    {
        /// <summary> Gets a list of campaign recipients </summary>
        /// <param name="campaignId"> Campaign ID represent the campaign that we want to get the recipients </param>
        /// <returns> List of campaign reciepients if fetch was successful, otherwise error message </returns>
        Task<Result<IList<CampaignRecipient>, string>> FetchCampaignRecipientsAsync(int campaignId);
    }
}
