using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignFeedViewModel : BaseViewModel
    {
        private readonly ICampaignService _campaignService;
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public CampaignFeedViewModel(ICampaignService campaignService,
                                     IAuthenticationService authenticationService,
                                     INavigationService navigationService)
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

        public Command RowSelectedCommand { get; set; }

        private int? _currentCampaignPage { get; set; }

        public int CurrentCampaignPageNumber
        {
            get => _currentCampaignPage ?? 0;
        }

        private int? _totalPageNumber { get; set; }

        public int TotalPageNumber
        {
            get => _totalPageNumber ?? int.MaxValue;
        }

        private Campaign _campaignSelected;

        public Campaign CampaignSelected
        {
            get => null;

            set
            {
                if (value != null)
                {
                    SetProperty(ref _campaignSelected, value);
                    NavigateToCampaignDetail(_campaignSelected);
                }
            }
        }

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
            return !IsBusy && Campaigns.Count != 0 && Campaigns.Last().CampaignId == item.CampaignId && HasMorePagesToShow();
        }

        public async void ExecuteFetchMoreCampaignsCommand(Campaign item)
        {
            var nextCampaignsPage = CurrentCampaignPageNumber + 1;
            await GetCampaigns(nextCampaignsPage);
        }

        public async Task ExecuteFetchCampaignsCommand()
        {
            Campaigns.Clear();
            var firstPage = 1;
            await GetCampaigns(firstPage);
        }

        private async Task NavigateToCampaignDetail(Campaign campaignSelected)
        {
            _navigationService.CurrentCampaign = campaignSelected;
            await _navigationService.NavigateToAsync<CampaignDetailViewModel>();
        }

        private async Task GetCampaigns(int pageNumber)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var campaignServiceResponse = await _campaignService.FetchCampaignsAsync(pageNumber);

            if (!campaignServiceResponse.IsSuccessResult)
                OnFetchCampaignFailed(campaignServiceResponse.ErrorValue);

            OnFetchCampaignSuccess(campaignServiceResponse.SuccessValue.Items, campaignServiceResponse.SuccessValue.CurrentPage, campaignServiceResponse.SuccessValue.PagesCount);
            IsBusy = false;
        }

        private void OnFetchCampaignFailed(string msg)
        {
            Application.Current.MainPage.DisplayAlert("", msg, "OK");
        }

        private void OnFetchCampaignSuccess(IList<Campaign> campaigns, int pageNumber, int totalPageNumber)
        {
            _currentCampaignPage = pageNumber;
            _totalPageNumber = totalPageNumber;
            campaigns.ToList().ForEach(Campaigns.Add);
        }

        private bool HasMorePagesToShow()
        {
            return CurrentCampaignPageNumber < TotalPageNumber;
        }
    }
}
