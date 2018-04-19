using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core;
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
            var getCampaignsResult = await campaignService.FetchCampaignsAsync(1);

            // Assert
            Assert.NotNull(getCampaignsResult.SuccessValue);
            Assert.Null(getCampaignsResult.ErrorValue);
            Assert.True(getCampaignsResult.IsSuccessResult);
            Assert.Equal(2,getCampaignsResult.SuccessValue.Items.Count);
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
            var getCampaignsResult = await campaignService.FetchCampaignsAsync(1);

            // Assert
            Assert.Null(getCampaignsResult.SuccessValue);
            Assert.NotNull(getCampaignsResult.ErrorValue);
            Assert.False(getCampaignsResult.IsSuccessResult);
            Assert.Equal(CoreResources.NotUserLoggedIn, getCampaignsResult.ErrorValue);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnPageCampaignModel_WhenApiGetCampaignsIsSuccessful()
        {
            // Arrange
            var campaignDto = Mocks.Mocks.GetCampaignDto(1);

            var pageToRet = new PageDto<CampaignDto>
            {
                Items = new CampaignDto[] { campaignDto },
                PageSize = 1,
                CurrentPage = 1,
                PagesCount = 1,
                ItemsCount = 1
            };


            var campaignModel = new Campaign
            {
                CampaignId = campaignDto.CampaignId,
                ScheduledDate = campaignDto.ScheduledDate,
                RecipientsRequired = campaignDto.RecipientsRequired,
                ContentRequired = campaignDto.ContentRequired,
                Name = campaignDto.Name,
                FromName = campaignDto.FromName,
                FromEmail = campaignDto.FromEmail,
                Subject = campaignDto.Subject,
                Preheader = campaignDto.Preheader,
                ReplyTo = campaignDto.ReplyTo,
                TextCampaign = campaignDto.TextCampaign,
                Status = campaignDto.Status
            };

            var pageCampaignModel = new Page<Campaign>
            {
                Items = new Campaign[] { campaignModel },
                PageSize = pageToRet.PageSize,
                CurrentPage = pageToRet.CurrentPage,
                PagesCount = pageToRet.PagesCount,
                ItemsCount = pageToRet.ItemsCount
            };
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.AccountNameLoggedIn).Returns("UserAccount");
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.GetCampaignsAsync(It.IsAny<string>(), It.IsAny<int>()))
                .ReturnsAsync(new Result<PageDto<CampaignDto>, string>(successValue: pageToRet));
            ICampaignService campaignService = new CampaignService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var getCampaignsResult = await campaignService.FetchCampaignsAsync(1);

            // Assert
            Assert.Equal(campaignModel.CampaignId, getCampaignsResult.SuccessValue.Items[0].CampaignId);
            Assert.Equal(campaignModel.Name, getCampaignsResult.SuccessValue.Items[0].Name);
            Assert.Equal(campaignModel.Subject, getCampaignsResult.SuccessValue.Items[0].Subject);
        }

        [Fact]
        public async Task GetCampaignsAsync_ShouldReturnError_WhenAPIReturnsAnError()
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
            var getCampaignsResult = await campaignService.FetchCampaignsAsync(1);

            // Assert
            Assert.NotNull(getCampaignsResult.ErrorValue);
            Assert.Null(getCampaignsResult.SuccessValue);
            Assert.False(getCampaignsResult.IsSuccessResult);
        }
    }
}
