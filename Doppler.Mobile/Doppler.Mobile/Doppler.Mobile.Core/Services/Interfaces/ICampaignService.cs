using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Networking;

namespace Doppler.Mobile.Core.Services
{
    /// <summary> Handles the campaign's information </summary>
    public interface ICampaignService
    {
        /// <summary> Gets the a list of campaigns related to page number and the user who is logged in </summary>
        Task<Result<Page<Campaign>, string>> FetchCampaignsAsync(int pageNumber);
    }
}
