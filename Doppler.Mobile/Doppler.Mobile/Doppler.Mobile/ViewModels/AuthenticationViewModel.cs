using System;
using System.Windows.Input;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Helper;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class AuthenticationViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
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
                var authResult = await _authenticationService.LoginAsync(Email, Password);

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
                Message = string.Empty;
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
                Message = string.Empty;
            }

            return validationSucceeded;
        }

        private bool ValidateAll()
        {
            return ValidateEmail() & ValidatePassword();
        }

        private void OnLoginFailed()
        {
            Message = AppResources.LoginView_InvalidCredentialsError;
        }

        private void OnLoginSuccess()
        {
            //TODO: create navigation
            Message = "BIEN!! PRONTO VAS A NAVEGAR";
        }
    }
}
