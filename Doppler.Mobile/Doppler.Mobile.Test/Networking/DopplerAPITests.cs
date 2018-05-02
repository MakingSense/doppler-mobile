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
                var userAuthenticationResponse = Mocks.Mocks.GetUserAuthenticationResponseDto();

                httpTest.RespondWithJson(userAuthenticationResponse);

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword", "apiKey");

                // Assert
                Assert.True(loginResult.IsSuccessResult);
                Assert.Null(loginResult.ErrorValue);
                Assert.NotNull(loginResult.SuccessValue);
                Assert.Equal(userAuthenticationResponse.AccessToken, loginResult.SuccessValue.AccessToken);
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
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword", "apiKey");

                // Assert
                Assert.False(loginResult.IsSuccessResult);
                Assert.NotNull(loginResult.ErrorValue);
                Assert.Null(loginResult.SuccessValue);
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
                var loginResult = await dopplerAPI.LoginAsync("TestUser@domain.com", "TestPassword", "apiKey");

                // Assert
                Assert.False(loginResult.IsSuccessResult);
                Assert.NotNull(loginResult.ErrorValue);
                Assert.Null(loginResult.SuccessValue);
                Assert.StartsWith(errorMsg, loginResult.ErrorValue);
            }
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnPage_WhenConnectionIsSuccessful()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble
            {
                ApiBaseUrl = "https://yahoo.com"
            };

            using (var httpTest = new HttpTest())
            {
                var getCampaignsResponse = Mocks.Mocks.GetPageCampaignDto();

                httpTest.RespondWithJson(getCampaignsResponse);

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var getCampaignsResult = await dopplerAPI.GetCampaignsAsync("Test@email.com", 1, "draft");

                // Assert
                Assert.True(getCampaignsResult.IsSuccessResult);
                Assert.Null(getCampaignsResult.ErrorValue);
                Assert.NotNull(getCampaignsResult.SuccessValue);
                Assert.Equal(2, getCampaignsResult.SuccessValue.Items.Count);
                Assert.Equal(getCampaignsResponse.Items[0].CampaignId, getCampaignsResult.SuccessValue.Items[0].CampaignId);
            }
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnString_WhenConnectionFailed()
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
                var getCampaignsResult  = await dopplerAPI.GetCampaignsAsync("TestUser@domain.com", 1, "draft");

                // Assert
                Assert.False(getCampaignsResult.IsSuccessResult);
                Assert.NotNull(getCampaignsResult.ErrorValue);
                Assert.Null(getCampaignsResult.SuccessValue);
                Assert.StartsWith(errorMsg, getCampaignsResult.ErrorValue);
            }
        }

        [Fact]
        public async Task GetCampaignRecipientsAsync_ShouldReturnPage_WhenConnectionIsSuccessful()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble
            {
                ApiBaseUrl = "https://yahoo.com"
            };

            using (var httpTest = new HttpTest())
            {
                var getCampaignRecipientsResponse = Mocks.Mocks.GetCampaignRecipientListDto();

                httpTest.RespondWithJson(getCampaignRecipientsResponse);

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var getCampaignRecipientsResult = await dopplerAPI.GetCampaignRecipientsAsync("Test@email.com", 1);

                // Assert
                Assert.True(getCampaignRecipientsResult.IsSuccessResult);
                Assert.Null(getCampaignRecipientsResult.ErrorValue);
                Assert.NotNull(getCampaignRecipientsResult.SuccessValue);
                Assert.Equal(getCampaignRecipientsResponse.Items.Count, getCampaignRecipientsResult.SuccessValue.Items.Count);
                Assert.Equal(getCampaignRecipientsResponse.Items[0].ListId, getCampaignRecipientsResult.SuccessValue.Items[0].ListId);
            }
        }

        [Fact]
        public async Task GetCampaignRecipientsAsync_ShouldReturnString_WhenConnectionFailed()
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
                var getCampaignRecipientsResult  = await dopplerAPI.GetCampaignRecipientsAsync("TestUser@domain.com", 1);

                // Assert
                Assert.False(getCampaignRecipientsResult.IsSuccessResult);
                Assert.NotNull(getCampaignRecipientsResult.ErrorValue);
                Assert.Null(getCampaignRecipientsResult.SuccessValue);
                Assert.StartsWith(errorMsg, getCampaignRecipientsResult.ErrorValue);
            }
        }

        [Fact]
        public async Task GetHtmlCampaignPreviewAsync_ShouldReturnHtmlString_WhenConnectionIsSuccessful()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            var configurationSettings = new ConfigurationSettingsDouble
            {
                ApiBaseUrl = "https://yahoo.com"
            };

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith("HTML HERE");

                IDopplerAPI dopplerAPI = new DopplerAPI(configurationSettings, localSettingsMock.Object);

                // Act
                var getHtmlCampaignPreviewResult = await dopplerAPI.GetCampaignHtmlPreviewAsync("Test@email.com", 12323);

                // Assert
                Assert.True(getHtmlCampaignPreviewResult.IsSuccessResult);
                Assert.Null(getHtmlCampaignPreviewResult.ErrorValue);
                Assert.NotNull(getHtmlCampaignPreviewResult.SuccessValue);
                Assert.NotEmpty(getHtmlCampaignPreviewResult.SuccessValue);
            }
        }

        [Fact]
        public async Task GetHtmlCampaignPreviewAsync_ShouldReturnErrorString_WhenConnectionFailed()
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
                var getHtmlCampaignPreviewResult  = await dopplerAPI.GetCampaignHtmlPreviewAsync("TestUser@domain.com", 12323);

                // Assert
                Assert.False(getHtmlCampaignPreviewResult.IsSuccessResult);
                Assert.NotNull(getHtmlCampaignPreviewResult.ErrorValue);
                Assert.Null(getHtmlCampaignPreviewResult.SuccessValue);
                Assert.StartsWith(errorMsg, getHtmlCampaignPreviewResult.ErrorValue);
            }
        }

    }
}
