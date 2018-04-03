using System;
using Doppler.Mobile.Core.Services;

namespace Doppler.Mobile.ViewModels
{
    public class HomeViewModel : IViewModel
    {
        public HomeViewModel(IHomeService homeService)
        {
            HelloText = homeService.GetHelloText();
        }

        public string HelloText { get; set; }
    }
}
