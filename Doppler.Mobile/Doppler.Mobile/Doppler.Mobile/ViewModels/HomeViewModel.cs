using System;

namespace Doppler.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            HelloText = "";
        }

        public string HelloText { get; set; }
    }
}
