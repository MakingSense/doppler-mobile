using System.Threading.Tasks;
using Doppler.Mobile.Core;
using Doppler.Mobile.Core.Models.Dto;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Moq;
using Xunit;

namespace Doppler.Mobile.Test.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task LoginAsync_ShouldReturnTrue_WhenAuthenticationIsSuccessful()
        {
            // Arrange
            var userAuthenticationResponseDto = Mocks.Mocks.GetUserAuthenticationResponseDto();
            var localSettingsMock = new Mock<ILocalSettings>();
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.LoginAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<UserAuthenticationResponseDto, string>(successValue: userAuthenticationResponseDto));
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var loginResult = await authenticationService.LoginAsync("TestUser@domain.com", "TestPassword", "apiKey");

            // Assert
            Assert.True(loginResult.IsSuccessResult);
            Assert.Null(loginResult.ErrorValue);
            Assert.NotNull(loginResult.SuccessValue);
            Assert.Equal(true, loginResult.SuccessValue);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnString_WhenAuthenticationFailed()
        {
            // Arrange
            var loginFailResult = "Fail";
            var localSettingsMock = new Mock<ILocalSettings>();
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.LoginAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<UserAuthenticationResponseDto, string>(errorValue: loginFailResult));
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var loginResult = await authenticationService.LoginAsync("TestUser@domain.com", "TestPassword", "apiKey");

            // Assert
            Assert.False(loginResult.IsSuccessResult);
            Assert.NotNull(loginResult.ErrorValue);
            Assert.False(loginResult.SuccessValue);
            Assert.Equal(loginFailResult, loginResult.ErrorValue);
        }

        [Fact]
        public void Logout_ShouldReturnTrue_WhenLogoutIsSuccessful()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.IsUserLoggedIn).Returns(true);
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var logoutResult = authenticationService.Logout();

            // Assert
            Assert.True(logoutResult.IsSuccessResult);
            Assert.Null(logoutResult.ErrorValue);
        }

        [Fact]
        public void Logout_ShouldReturnErrorMsg_WhenLogoutFailed()
        {
            // Arrange
            var localSettingsMock = new Mock<ILocalSettings>();
            localSettingsMock.SetupGet(ls => ls.IsUserLoggedIn).Returns(false);
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var logoutResult = authenticationService.Logout();

            // Assert
            Assert.False(logoutResult.IsSuccessResult);
            Assert.Equal(CoreResources.NotUserLoggedIn, logoutResult.ErrorValue);
        }
    }
}
