using System;
using System.Collections.Generic;
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
                SaveLoggedAccountInfo(user.AccessToken, user.Username);
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

        /// <inheritdoc />
        public async Task<Result<PageDto<CampaignDto>, string>> GetCampaignsAsync(string accountName, int pageNumber)
        {
            var url = _configuration.ApiBaseUrl + $"accounts/{accountName}/campaigns?page={pageNumber}";
            var token = "CCC8C153443B33C1062BE28837DCC549";//GetAccessToken();
            try
            {
                var page = await url.WithHeader("Authorization", $"token {token}").GetAsync().ReceiveJson<PageDto<CampaignDto>>();

                return new Result<PageDto<CampaignDto>, string>(page);
            }
            catch (FlurlHttpException ex)
            {
                try
                {
                    var dopplerError = await ex.GetResponseJsonAsync<DopplerErrorDto>();
                    return new Result<PageDto<CampaignDto>, string>(dopplerError.Detail);
                }
                catch
                {
                    return new Result<PageDto<CampaignDto>, string>(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new Result<PageDto<CampaignDto>, string>(ex.Message);
            }
        }

        private void SaveLoggedAccountInfo(string token, string accountName)
        {
            _settings.AuthAccessToken = token;
            _settings.AccountNameLoggedIn = accountName;
        }

        private string GetAccessToken()
        {
            return _settings.AuthAccessToken;
        }
    }
}