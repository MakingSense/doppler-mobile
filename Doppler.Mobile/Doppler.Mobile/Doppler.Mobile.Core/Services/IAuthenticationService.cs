using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Networking;

namespace Doppler.Mobile.Core.Services
{
    /// <summary> User authentication </summary>
    public interface IAuthenticationService
    {
        /// <summary> Authenticates API with username and password </summary>
        /// <param name="username"> Username to authenticate with </param>
        /// <param name="password"> Password to authenticate with </param>
        /// <returns> True if login was successful, otherwise error message </returns>
        Task<Result<bool, string>> LoginAsync(string username, string password);
    }
}
