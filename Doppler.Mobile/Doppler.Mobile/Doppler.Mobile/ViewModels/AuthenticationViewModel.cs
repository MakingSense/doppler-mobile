using System.Windows.Input;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Helper;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class AuthenticationViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public AuthenticationViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            LoginCommand  = new Command(LoginAsync);
        }

        public ICommand LoginCommand { protected set; get; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public async void LoginAsync()
        {
            if (ValidateAll())
            {
                IsBusy = true;
                var authResult = await _authenticationService.LoginAsync("nachocrespo81@yahoo.com", "Nacho3636");

                if (authResult.IsSuccessResult)
                    OnLoginSuccess();
                else
                    OnLoginFailed();

                IsBusy = false;
            }
        }

        private bool ValidateEmail()
        {
            var validationSucceeded = false;
            if (string.IsNullOrEmpty(Email))
            {
                Message = AppResources.LoginView_EmailEmpty;
            }
            else if (!EmailHelper.EmailIsValid(Email))
            {
                Message = AppResources.LoginView_EmailInvalid;
            }
            else
            {
                validationSucceeded = true;
            }

            return validationSucceeded;
        }

        private bool ValidatePassword()
        {
            var validationSucceeded = false;
            if (string.IsNullOrEmpty(Password))
            {
                Message = AppResources.LoginView_PasswordEmpty;
            }
            else
            {
                validationSucceeded = true;
            }

            return validationSucceeded;
        }

        private bool ValidateAll()
        {
            Message = string.Empty;
            return ValidateEmail() && ValidatePassword();
        }

        private void OnLoginFailed()
        {
            Message = AppResources.LoginView_InvalidCredentialsError;
        }

        private void OnLoginSuccess()
        {
            _navigationService.NavigateInNewStackToAsync<CampaignFeedViewModel>();
            _navigationService.RemoveLastFromBackStackAsync();
        }
    }
}
