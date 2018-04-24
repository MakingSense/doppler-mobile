using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Networking;

namespace Doppler.Mobile.Core.Services
{
    /// <summary> Handles the campaign's information </summary>
    public interface ICampaignService
    {
        /// <summary> Gets a list of campaigns </summary>
        /// <param name="pageNumber"> Page number represents number of page that we want to get </param>
        /// <returns> List of campaigns if fetch was successful, otherwise error message </returns>
        Task<Result<Page<Campaign>, string>> FetchCampaignsAsync(int pageNumber);
    }
}
