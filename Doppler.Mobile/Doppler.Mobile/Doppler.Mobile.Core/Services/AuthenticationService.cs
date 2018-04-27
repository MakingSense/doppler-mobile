using System.Threading.Tasks;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Models.Dto;

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
        public async Task<Result<bool, string>> LoginAsync(string username, string password, string apiKey)
        {
            var loginResponse = await _dopplerApi.LoginAsync(username, password, apiKey);

            if (!loginResponse.IsSuccessResult)
                return new Result<bool, string>(loginResponse.ErrorValue);

            var user = loginResponse.SuccessValue;
            SaveLoggedAccountInfo(user, apiKey);

            return new Result<bool, string>(true);
        }

        /// <inheritdoc />
        public Result<bool, string> Logout()
        {
            if (!_localSettings.IsUserLoggedIn)
                return new Result<bool, string>(errorValue: CoreResources.NotUserLoggedIn);

            _localSettings.AuthAccessToken = string.Empty;
            _localSettings.AccountNameLoggedIn = string.Empty;

            return new Result<bool, string>(successValue: true);
        }

        private void SaveLoggedAccountInfo(UserAuthenticationResponseDto user, string apiKey)
        {
            _localSettings.AuthAccessToken = apiKey;
            _localSettings.AccountNameLoggedIn = user.Username;
            _localSettings.AccountIdLoggedIn = user.AccountId;
        }
    }
}
