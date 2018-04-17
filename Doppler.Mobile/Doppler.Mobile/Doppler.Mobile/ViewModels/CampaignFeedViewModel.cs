using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignFeedViewModel: BaseViewModel
    {
        private readonly ICampaignService _campaignService;
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public CampaignFeedViewModel(ICampaignService campaignService, IAuthenticationService authenticationService, INavigationService navigationService)
        {
            Title = "Campaigns";
            _campaignService = campaignService;
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            Campaigns = new ObservableCollection<Campaign>();
            LoadCampaignsCommand = new Command(async () => await ExecuteFetchCampaignsCommand());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
            LoadMoreCampaignsCommand = new Command<Campaign>(ExecuteFetchMoreCampaignsCommand, CanExecuteLoadMoreCommand);
        }

        public ObservableCollection<Campaign> Campaigns { get; set; }

        public Command LoadCampaignsCommand { get; set; }

        public Command LoadMoreCampaignsCommand { private set; get; }

        public Command LogoutCommand { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            await ExecuteFetchCampaignsCommand();
        }

        public async Task ExecuteLogoutCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            _authenticationService.Logout();
            await _navigationService.NavigateInNewStackToAsync<AuthenticationViewModel>();
            await _navigationService.RemoveBackStackAsync();
            IsBusy = false;
        }

        public bool CanExecuteLoadMoreCommand(Campaign item)
        {
            return !IsBusy && Campaigns.Count != 0 && Campaigns.Last().CampaignId == item.CampaignId;
        }

        public async void ExecuteFetchMoreCampaignsCommand(Campaign item)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var campaigns = await _campaignService.GetMoreCampaignsAsync();
                foreach (var campaign in campaigns)
                {
                    Campaigns.Add(campaign);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task ExecuteFetchCampaignsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Campaigns.Clear();
                var campaigns = await _campaignService.GetCampaignsAsync();
                foreach (var campaign in campaigns)
                {
                    Campaigns.Add(campaign);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
