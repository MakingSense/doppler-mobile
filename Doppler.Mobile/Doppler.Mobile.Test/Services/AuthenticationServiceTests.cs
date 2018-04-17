using System;
using System.Threading.Tasks;
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
            var localSettingsMock = new Mock<ILocalSettings>();
            var dopplerAPIMock = new Mock<IDopplerAPI>();
            dopplerAPIMock
                .Setup(dAPI => dAPI.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool, string>(successValue: true));
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var loginResult = await authenticationService.LoginAsync("TestUser@domain.com", "TestPassword");

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
                .Setup(dAPI => dAPI.LoginAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Result<bool, string>(errorValue: loginFailResult));
            IAuthenticationService authenticationService = new AuthenticationService(localSettingsMock.Object, dopplerAPIMock.Object);

            // Act
            var loginResult = await authenticationService.LoginAsync("TestUser@domain.com", "TestPassword");

            // Assert
            Assert.False(loginResult.IsSuccessResult);
            Assert.NotNull(loginResult.ErrorValue);
            Assert.False(loginResult.SuccessValue);
            Assert.Equal(loginFailResult, loginResult.ErrorValue);
        }
    }
}
