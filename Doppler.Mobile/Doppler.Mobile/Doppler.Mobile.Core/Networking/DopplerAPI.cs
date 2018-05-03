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
        private readonly ILocalSettings _localSettings;

        public DopplerAPI(IConfigurationSettings configuration, ILocalSettings localSettings)
        {
            _configuration = configuration;
            _localSettings = localSettings;
        }

        /// <inheritdoc />
        public async Task<Result<UserAuthenticationResponseDto, string>> LoginAsync(string username, string password, string apiKey)
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

                return new Result<UserAuthenticationResponseDto, string>(user);
            }
            catch (FlurlHttpException ex)
            {
                try
                {
                    var dopplerError = await ex.GetResponseJsonAsync<DopplerErrorDto>();
                    return new Result<UserAuthenticationResponseDto, string>(dopplerError.Detail);
                }
                catch
                {
                    return new Result<UserAuthenticationResponseDto, string>(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new Result<UserAuthenticationResponseDto, string>(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<Result<PageDto<CampaignDto>, string>> GetCampaignsAsync(string accountName, int pageNumber, string campaignType)
        {
            var url = _configuration.ApiBaseUrl + $"accounts/{accountName}/campaigns?page={pageNumber}&state={campaignType}";
            var token = GetAccessToken();
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

        /// <inheritdoc />
        public async Task<Result<CampaignRecipientListDto, string>> GetCampaignRecipientsAsync(string accountName, int campaignId)
        {
            var url = _configuration.ApiBaseUrl + $"accounts/{accountName}/campaigns/{campaignId}/recipients";
            var token = GetAccessToken();
            try
            {
                var campaignRecipients = await url.WithHeader("Authorization", $"token {token}").GetAsync().ReceiveJson<CampaignRecipientListDto>();

                return new Result<CampaignRecipientListDto, string>(campaignRecipients);
            }
            catch (FlurlHttpException ex)
            {
                try
                {
                    var dopplerError = await ex.GetResponseJsonAsync<DopplerErrorDto>();
                    return new Result<CampaignRecipientListDto, string>(dopplerError.Detail);
                }
                catch
                {
                    return new Result<CampaignRecipientListDto, string>(ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new Result<CampaignRecipientListDto, string>(ex.Message);
            }
        }

        /// <inheritdoc />
        public async Task<Result<string, string>> GetCampaignHtmlPreviewAsync(string accountName, int campaignId)
        {
            var url = _configuration.ApiBaseUrl + $"accounts/{accountName}/campaigns/{campaignId}/content";
            var token = GetAccessToken();
            try
            {
                var html = await url.WithHeader("Authorization", $"token {token}").GetAsync().ReceiveString();


                return new Result<string, string>(successValue: html);
            }
            catch (FlurlHttpException ex)
            {
                try
                {
                    var dopplerError = await ex.GetResponseJsonAsync<DopplerErrorDto>();
                    return new Result<string, string>(errorValue: dopplerError.Detail);
                }
                catch
                {
                    return new Result<string, string>(errorValue: ex.Message);
                }
            }
            catch (Exception ex)
            {
                return new Result<string, string>(errorValue: ex.Message);
            }
        }

        private string GetAccessToken()
        {
            return _localSettings.AuthAccessToken;
        }
    }
}