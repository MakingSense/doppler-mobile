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
        /// <param name="campaignType"> Type of campaign to get campaign list </param>
        /// <returns> Campagin list related to the account name and campaignType if request was successful, otherwise error message </returns>
        Task<Result<PageDto<CampaignDto>, string>> GetCampaignsAsync(string accountName, int pageNumber, string campaignType);

        /// <summary> Gets a campaign recipient list </summary>
        /// <param name="accountName"> Account name to get campaign list </param>
        /// <param name="campaignId"> Campaign ID represent the campaign that we want to get the recipients </param>
        /// <returns> Campagin recipient list related to the account name and campaign ID if request was successful, otherwise error message </returns>
        Task<Result<CampaignRecipientListDto, string>> GetCampaignRecipientsAsync(string accountName, int campaignId);

        /// <summary> Gets a html campaign preview </summary>
        /// <param name="accountName"> Account name to get campaign list </param>
        /// <param name="campaignId"> Campaign id to get campaign html preview</param>
        /// <returns> Html campagin preview related to the account name and campaignId if request was successful, otherwise error message </returns>
        Task<Result<string, string>> GetCampaignHtmlPreviewAsync(string accountName, int campaignId);
    }
}
