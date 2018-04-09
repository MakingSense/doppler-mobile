using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Settings;
using Flurl.Http.Testing;
using Moq;
using Xunit;

namespace Doppler.Mobile.Test.Networking
{
    public class DopplerAPITests
    {
        [Fact]
        public async Task LoginAsync_ShouldReturnTrue_WhenAuthenticationIsSuccessful()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble {
                ApiBaseUrl = "https://yahoo.com/"
            };

            using (var httpTest = new HttpTest())
            {
                var userAuthenticationResponse = new UserAuthenticationResponseDto
                {
                    AccessToken = "AccessToken",
                    AccountId = 1,
                    Username = "userTest",
                    IssuedAt = "testDate",
                    ExpirationDate = "Expiration Date"
                };

                httpTest.RespondWithJson(userAuthenticationResponse);

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword");

                // Assert
                Assert.True(loginResult.IsSuccessResult);
                Assert.Null(loginResult.ErrorValue);
                Assert.NotNull(loginResult.SuccessValue);
                Assert.Equal(true, loginResult.SuccessValue);
            }
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnDopplerErrorMessage_WhenAuthenticationFailed()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble
            {
                ApiBaseUrl = "https://yahoo.com/"
            };

            using (var httpTest = new HttpTest())
            {
                var dopplerError = new DopplerErrorDto
                {
                    Title = "Authentication error",
                    Status = 401,
                    ErrorCode = 2,
                    Detail = "Authentication credentials are invalid",
                    Type = "/docs/errors/401.2-authentication-error"
                };
                httpTest.RespondWithJson(dopplerError, 401);
                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword");

                // Assert
                Assert.False(loginResult.IsSuccessResult);
                Assert.NotNull(loginResult.ErrorValue);
                Assert.False(loginResult.SuccessValue);
                Assert.Equal(dopplerError.Detail, loginResult.ErrorValue);
            }
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnString_WhenConnectionFailed()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble
            {
                ApiBaseUrl = "https://yahoo.com"
            };

            using (var httpTest = new HttpTest())
            {
                var errorMsg = "Call failed with status code 500 (Internal Server Error)";
                httpTest.RespondWith(errorMsg, 500);

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword");

                // Assert
                Assert.False(loginResult.IsSuccessResult);
                Assert.NotNull(loginResult.ErrorValue);
                Assert.False(loginResult.SuccessValue);
                Assert.StartsWith(errorMsg, loginResult.ErrorValue);
            }
        }
    }
}
