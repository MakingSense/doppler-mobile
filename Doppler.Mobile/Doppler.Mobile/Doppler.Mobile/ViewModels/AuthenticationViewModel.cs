using System;
using System.Windows.Input;
using Doppler.Mobile.Core.Services;
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
            IsBusy = true;
            Message = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    if (!string.IsNullOrEmpty(Password))
                    {
                        var result = await _authenticationService.LoginAsync("nachocrespo81@yahoo.com", "Nacho3636");
                        if (result.IsSuccessResult)
                        {
                            //TODO: implement navigation
                            Message = "BIEN!! PRONTO VAS A NAVEGAR";
                        }
                        else
                        {
                            Message = "Usuario o contraseña incorrecta";
                        }
                        IsBusy = false;
                    }
                    else
                    {
                        IsBusy = false;
                        Message = "La contraseña es requerido";
                    }

                }
                else
                {
                    IsBusy = false;
                    Message = "El email es requerido";
                }

            }
            catch (Exception e)
            {
                IsBusy = false;
                await App.Current.MainPage.DisplayAlert("Error de conexión", e.Message, "Ok");
            }
        }
    }
}
