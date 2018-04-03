using System;
using Doppler.Mobile.ViewModels;
using Xamarin.Forms;

namespace Doppler.Mobile.Views
{
    public class HomePage : ViewPage<HomeViewModel>
    {
        public HomePage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = ViewModel.HelloText }
                }
            };
        }
    }
}

