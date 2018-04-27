using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models.Dto;

namespace Doppler.Mobile.Core.Networking
{
    /// <summary> Handles the connection with Doppler's API</summary>
    public interface IDopplerAPI
    {
        /// <summary> Authenticates with username and password </summary>
        /// <param name="username"> Username to authenticate with </param>
        /// <param name="password"> Password to authenticate with </param>
        /// <param name="apiKey"> Api key to use the doppler api </param>
        /// <returns> User if login was successful, otherwise error message </returns>
        Task<Result<UserAuthenticationResponseDto, string>> LoginAsync(string username, string password, string apiKey);

        /// <summary> Gets a campaign list with account name and number of page </summary>
        /// <param name="accountName"> Account name to get campaign list </param>
        /// <param name="pageNumber"> Page number to get campaign list </param>
        /// <returns> Campagin list related to the account name if request was successful, otherwise error message </returns>
        Task<Result<PageDto<CampaignDto>, string>> GetCampaignsAsync(string accountName, int pageNumber);

        /// <summary> Gets a campaign list with account name and number of page </summary>
        /// <param name="accountName"> Account name to get campaign list </param>
        /// <param name="campaignId"> Campaign ID represent the campaign that we want to get the recipients </param>
        /// <returns> Campagin recipient list related to the account name and campaign ID if request was successful, otherwise error message </returns>
        Task<Result<CampaignRecipientListDto, string>> GetCampaignRecipientsAsync(string accountName, int campaignId);
    }
}
