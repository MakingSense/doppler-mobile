using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Networking;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Core.Settings;
using Doppler.Mobile.ViewModels;
using Moq;
using Xunit;

namespace Doppler.Mobile.Test.ViewModels
{
    public class AuthenticationViewModelTests
    {
        [Fact]
        public async Task LoginAsync_ShouldShowEmailEmptyError_WhenEmailIsEmpty()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthenticationService>();
            var authenticationViewModel = new AuthenticationViewModel(authServiceMock.Object);

            // Act
            authenticationViewModel.LoginAsync();

            // Assert
            Assert.Equal(AppResources.LoginView_EmailEmpty, authenticationViewModel.Message);
            Assert.True(string.IsNullOrEmpty(authenticationViewModel.Email));
        }

        [Fact]
        public async Task LoginAsync_ShouldShowEmailInvalidError_WhenEmailIsNotValid()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthenticationService>();
            var authenticationViewModel = new AuthenticationViewModel(authServiceMock.Object);
            authenticationViewModel.Email = "NotEmail";

            // Act
            authenticationViewModel.LoginAsync();

            // Assert
            Assert.Equal(AppResources.LoginView_EmailInvalid, authenticationViewModel.Message);
            Assert.False(string.IsNullOrEmpty(authenticationViewModel.Email));
        }

        [Fact]
        public async Task LoginAsync_ShouldShowPasswordError_WhenPasswordIsEmpty()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthenticationService>();
            var authenticationViewModel = new AuthenticationViewModel(authServiceMock.Object);
            authenticationViewModel.Email = "email@Test.com";

            // Act
            authenticationViewModel.LoginAsync();

            // Assert
            Assert.Equal(AppResources.LoginView_PasswordEmpty, authenticationViewModel.Message);
            Assert.True(string.IsNullOrEmpty(authenticationViewModel.Password));
        }

        [Fact]
        public async Task LoginAsync_ShouldShowCredentialInvalid_WhenServerLoginFailed()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(auth => auth.LoginAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new Result<bool, string>(errorValue: "errorMsg"));
            var authenticationViewModel = new AuthenticationViewModel(authServiceMock.Object);
            authenticationViewModel.Email = "test@Email.com";
            authenticationViewModel.Password = "Password";

            // Act
            authenticationViewModel.LoginAsync();

            // Assert
            Assert.Equal(AppResources.LoginView_InvalidCredentialsError, authenticationViewModel.Message);
            Assert.False(string.IsNullOrEmpty(authenticationViewModel.Password));
            Assert.False(string.IsNullOrEmpty(authenticationViewModel.Email));
        }

        [Fact]
        public async Task LoginAsync_ShouldNotShowAnyError_WhenServerLoginIsSuccessful()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(auth => auth.LoginAsync(It.IsAny<String>(), It.IsAny<String>()))
                .ReturnsAsync(new Result<bool, string>(true));
            var authenticationViewModel = new AuthenticationViewModel(authServiceMock.Object);
            authenticationViewModel.Email = "test@Email.com";
            authenticationViewModel.Password = "Password";

            // Act
            authenticationViewModel.LoginAsync();

            // Assert
            Assert.True(string.IsNullOrEmpty(authenticationViewModel.Message));
            Assert.False(string.IsNullOrEmpty(authenticationViewModel.Password));
            Assert.False(string.IsNullOrEmpty(authenticationViewModel.Email));
        }
    }
}
