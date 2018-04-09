using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.Core.Networking;

namespace Doppler.Mobile.Core.Services
{
    /// <inheritdoc />
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILocalSettings _localSettings;
        private readonly IDopplerAPI _dopplerApi;

        public AuthenticationService(ILocalSettings localSettings, IDopplerAPI dopplerApi)
        {
            _localSettings = localSettings;
            _dopplerApi = dopplerApi;
        }

        /// <inheritdoc />
        public async Task<Result<bool, string>> LoginAsync(string username, string password)
        {
            var loginResponse = await _dopplerApi.LoginAsync(username, password);

            if (!loginResponse.IsSuccessResult)
                return new Result<bool, string>(loginResponse.ErrorValue);

            SaveCredentials();
            return new Result<bool, string>(true);
        }

        private void SaveCredentials()
        {
            _localSettings.AddOrUpdateValue(LocalSettingsKeys.IsUserLoggedIn, true);
        }
    }
}
