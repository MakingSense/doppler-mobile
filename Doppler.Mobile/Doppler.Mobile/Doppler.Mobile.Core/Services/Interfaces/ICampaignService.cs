using System.Collections.Generic;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;

namespace Doppler.Mobile.Core.Services
{
    /// <summary> Handles the campaign's information </summary>
    public interface ICampaignService
    {
        /// <summary> Gets the first list of campaigns related to the user who is logged in </summary>
        Task<IList<Campaign>> GetCampaignsAsync();

        /// <summary> Gets the next list of campaigns related to the user who is logged in </summary>
        Task<IList<Campaign>> GetMoreCampaignsAsync();

        int CurrentCampaignPageNumber { get; }
    }
}
