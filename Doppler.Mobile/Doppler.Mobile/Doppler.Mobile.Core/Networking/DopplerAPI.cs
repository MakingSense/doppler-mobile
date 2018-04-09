using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Configuration;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Settings;
using Flurl.Http;

namespace Doppler.Mobile.Core.Networking
{
    /// <inheritdoc />
    public class DopplerAPI : IDopplerAPI
    {
        private readonly IConfigurationSettings _configuration;
        private readonly ILocalSettings _settings;

        public DopplerAPI(IConfigurationSettings configuration, ILocalSettings settings)
        {
            _configuration = configuration;
            _settings = settings;
        }

        /// <inheritdoc />
        public async Task<Result<bool, string>> LoginAsync(string username, string password)
        {
            var userAuthentication = new UserAuthenticationDto
            {
                GrantType = "password",
                Username = username,
                Password = password
            };

            var url = _configuration.ApiBaseUrl + "/tokens";
            try
            {
                var user = await url.PostJsonAsync(userAuthentication).ReceiveJson<UserAuthenticationResponseDto>();
                SaveToken(user.AccessToken);
                return new Result<bool, string>(true);
            }
            catch (FlurlHttpException ex)
            {
                try
                {
                    var dopplerError = await ex.GetResponseJsonAsync<DopplerErrorDto>();
                    return new Result<bool, string>(dopplerError.Detail);
                }
                catch
                {
                    return new Result<bool, string>(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new Result<bool, string>(ex.Message);
            }
        }

        private void SaveToken(string token)
        {
            //TODO: save token on device
        }
    }
}