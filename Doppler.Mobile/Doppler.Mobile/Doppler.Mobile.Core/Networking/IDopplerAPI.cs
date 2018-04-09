using System.Threading.Tasks;

namespace Doppler.Mobile.Core.Networking
{
    /// <summary> Handles the connection with Doppler's API</summary>
    public interface IDopplerAPI
    {
        /// <summary> Authenticates with username and password </summary>
        /// <param name="username"> Username to authenticate with </param>
        /// <param name="password"> Password to authenticate with </param>
        /// <returns> True if login was successful, otherwise error message </returns>
        Task<Result<bool, string>> LoginAsync(string username, string password);
    }
}
