using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Moq;
using Xunit;

namespace Doppler.Mobile.Test.Services
{
    public class CampaignRecipientServiceTest
    {
        [Fact]
        public async Task FetchCampaignRecipientsAsync_ShouldReturnCampaignList_WhenUserIsLoggedIn_AndApiGetCampaignsIsSuccessful()
        {
            // Arrange
            var getCampaignRecipientsResponse = Mocks.Mocks.GetCampaignRecipientListDto();
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignRecipientsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<CampaignRecipientListDto, string>(successValue: getCampaignRecipientsResponse));
            ICampaignRecipientService campaignRecipientService = new CampaignRecipientService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignRecipientsResult = await campaignRecipientService.FetchCampaignRecipientsAsync(234);

            // Assert
            Assert.NotNull(getCampaignRecipientsResult.SuccessValue);
            Assert.Null(getCampaignRecipientsResult.ErrorValue);
            Assert.True(getCampaignRecipientsResult.IsSuccessResult);
            Assert.Equal(getCampaignRecipientsResponse.Items.Count, getCampaignRecipientsResult.SuccessValue.Count);
        }

        [Fact]
        public async Task FetchCampaignRecipientsAsync_ShouldReturnError_WhenAPIReturnsAnError()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignRecipientsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<CampaignRecipientListDto, string>(errorValue: "ERROR"));
            ICampaignRecipientService campaignRecipientService = new CampaignRecipientService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignRecipientsResult = await campaignRecipientService.FetchCampaignRecipientsAsync(1);

            // Assert
            Assert.NotNull(getCampaignRecipientsResult.ErrorValue);
            Assert.Null(getCampaignRecipientsResult.SuccessValue);
            Assert.False(getCampaignRecipientsResult.IsSuccessResult);
        }
    }
}
