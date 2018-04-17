using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Moq;
using Xunit;

namespace Doppler.Mobile.Test.Services
{
    public class CampaignServiceTest
    {
        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnCampaignList_WhenUserIsLoggedIn_AndApiGetCampaignsIsSuccessful()
        {
            // Arrange
            var pageToRet = Mocks.Mocks.GetPageCampaignDto();
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignsResult = await campaignService.GetCampaignsAsync();

            // Assert
            Assert.NotNull(getCampaignsResult);
            Assert.Equal(2,getCampaignsResult.Count);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnEmptyCampaignList_WhenUserIsNotLoggedIn()
        {
            // Arrange
            var pageToRet = Mocks.Mocks.GetPageCampaignDto();
            var localSettingsMock = new Mock<ILocalSettings>();
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignsResult = await campaignService.GetCampaignsAsync();

            // Assert
            Assert.NotNull(getCampaignsResult);
            Assert.Equal(0, getCampaignsResult.Count);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnCampaignModelList_WhenApiGetCampaignsIsSuccessful()
        {
            // Arrange
            var campaign1Dto = Mocks.Mocks.GetCampaignDto(1);

            var campaign1Model = new Campaign
            {
                CampaignId = campaign1Dto.CampaignId,
                ScheduledDate = campaign1Dto.ScheduledDate,
                RecipientsRequired = campaign1Dto.RecipientsRequired,
                ContentRequired = campaign1Dto.ContentRequired,
                Name = campaign1Dto.Name,
                FromName = campaign1Dto.FromName,
                FromEmail = campaign1Dto.FromEmail,
                Subject = campaign1Dto.Subject,
                Preheader = campaign1Dto.Preheader,
                ReplyTo = campaign1Dto.ReplyTo,
                TextCampaign = campaign1Dto.TextCampaign,
                Status = campaign1Dto.Status
            };

            var pageToRet = new PageDto<CampaignDto>
            {
                Items = new CampaignDto[] { campaign1Dto },
                PageSize = 1,
                CurrentPage = 1,
                PagesCount = 1,
                ItemsCount = 1
            };
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignsResult = await campaignService.GetCampaignsAsync();

            // Assert
            Assert.Equal(campaign1Model.CampaignId, getCampaignsResult[0].CampaignId);
            Assert.Equal(campaign1Model.Name, getCampaignsResult[0].Name);
            Assert.Equal(campaign1Model.Subject, getCampaignsResult[0].Subject);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnEmptyList_WhenAPIReturnsAnError()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(errorValue: "ERROR"));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignsResult = await campaignService.GetCampaignsAsync();

            // Assert
            Assert.NotNull(getCampaignsResult);
            Assert.Equal(0, getCampaignsResult.Count);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldUpdateThePageCount_WhenApiGetCampaignsIsSuccessful()
        {
            // Arrange
            var pageToRet = Mocks.Mocks.GetPageCampaignDto();
            var page2ToRet = Mocks.Mocks.GetPageCampaignDto();
            page2ToRet.CurrentPage = 2;
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .SetupSequence(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: page2ToRet));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);
            var firstCampaignFetch = await campaignService.GetCampaignsAsync();

            // Act
            var secondCampaignFetch = await campaignService.GetMoreCampaignsAsync();

            // Assert
            Assert.Equal(2,firstCampaignFetch.Count);
            Assert.Equal(2, secondCampaignFetch.Count);
            Assert.Equal(2, campaignService.CurrentCampaignPageNumber);
        }

        public async Task GetCampaignsAsync_ShouldNotUpdateThePageCount_WhenApiGetCampaignsFailed()
        {
            // Arrange
            var pageToRet = Mocks.Mocks.GetPageCampaignDto();
            var page2ToRet = Mocks.Mocks.GetPageCampaignDto();
            page2ToRet.CurrentPage = 2;
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .SetupSequence(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(errorValue: "ERROR"));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);
            var firstCampaignFetch = await campaignService.GetCampaignsAsync();

            // Act
            var secondCampaignFetch = await campaignService.GetMoreCampaignsAsync();

            // Assert
            Assert.Equal(2, firstCampaignFetch.Count);
            Assert.Equal(2, secondCampaignFetch.Count);
            Assert.Equal(1, campaignService.CurrentCampaignPageNumber);
        }
    }
}
