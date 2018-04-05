using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Configuration;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Settings;
using Newtonsoft.Json;

namespace Doppler.Mobile.Core.Networking
{
    /// <inheritdoc />
    public class DopplerAPI : IDopplerAPI
    {
        private HttpClient _client;
        private readonly IConfigurationSettings _configuration;
        private readonly ILocalSettings _settings;

        public DopplerAPI(IConfigurationSettings configuration, ILocalSettings settings)
        {
            _configuration = configuration;
            _settings = settings;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration.ApiBaseUrl);
        }

        private void SetHeaderAuthorization(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", token);
        }

        /// <inheritdoc />
        public async Task<Result<bool, string>> LoginAsync(string username, string password)
        {
            var userAuthentication = new UserAuthenticationDto
            {
                grant_type = "password",
                username = username,
                password = password
            };

            var content = new JsonContent(userAuthentication, Encoding.UTF8);
            var loginResponse = await _client.PostAsync("/tokens", content);

            if (loginResponse.IsSuccessStatusCode)
            {
                var loginResponseString = await loginResponse.Content.ReadAsStringAsync();
                var userAuthResponse = JsonConvert.DeserializeObject<UserAuthenticationResponseDto>(loginResponseString);
                SetHeaderAuthorization(userAuthResponse.access_token);
                return new Result<bool, string>(true);
            }

            return new Result<bool, string>(loginResponse.ReasonPhrase);
        }
    }
}