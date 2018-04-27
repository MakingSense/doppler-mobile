using System.Threading.Tasks;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignDetailViewModel : BaseViewModel
    {
        private readonly CampaignBasicInfoViewModel _campaignBasicInfoViewModel;
        private readonly CampaignRecipientsInfoViewModel _campaignRecipientsInfoViewModel;
        private readonly CampaignSendingInfoViewModel _campaignSendingInfoViewModel;
        private readonly INavigationService _navigationService;
        private readonly ICampaignService _campaignService;

        public CampaignDetailViewModel(CampaignBasicInfoViewModel campaignBasicInfoViewModel,
                                       CampaignRecipientsInfoViewModel campaignRecipientsInfoViewModel,
                                       CampaignSendingInfoViewModel campaignSendingInfoViewModel,
                                       INavigationService navigationService,
                                       ICampaignService campaignService)
        {
            PreviewCommand = new Command(async () => await ExecutePreviewCommand());
            _campaignService = campaignService;
            _campaignBasicInfoViewModel = campaignBasicInfoViewModel;
            _campaignRecipientsInfoViewModel = campaignRecipientsInfoViewModel;
            _campaignSendingInfoViewModel = campaignSendingInfoViewModel;
            _navigationService = navigationService;
        }

        public CampaignBasicInfoViewModel CampaignBasicInfoViewModel
        {
            get => _campaignBasicInfoViewModel;
        }

        public CampaignRecipientsInfoViewModel CampaignRecipientsInfoViewModel
        {
            get => _campaignRecipientsInfoViewModel;
        }

        public CampaignSendingInfoViewModel CampaignSendingInfoViewModel
        {
            get => _campaignSendingInfoViewModel;
        }

        private string _campaignType;
        public string CampaignType
        {
            get { return _campaignType; }
            set { SetProperty(ref _campaignType, value); }
        }

        private string _campaignName;
        public string CampaignName
        {
            get { return _campaignName; }
            set { SetProperty(ref _campaignName, value); }
        }

        private int _tabSelected;
        public int TabSelected
        {
            get => _tabSelected;

            set
            {
                SetProperty(ref _tabSelected, value);
                OnPropertyChanged("IsTabOneVisible");
                OnPropertyChanged("IsTabTwoVisible");
                OnPropertyChanged("IsTabThreeVisible");
            }
        }

        public bool IsTabOneVisible
        {
            get => TabSelected == 0;
        }

        public bool IsTabTwoVisible
        {
            get => TabSelected == 1;
        }

        public bool IsTabThreeVisible
        {
            get => TabSelected == 2;
        }

        public Command PreviewCommand { get; set; }

        public async Task ExecutePreviewCommand()
        {
            await _navigationService.NavigateToAsync<CampaignPreviewViewModel>();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                CampaignName = currentCampaign.Name;
                CampaignType = currentCampaign.Status;
            }
        }
    }
}
