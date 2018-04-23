using System.Threading.Tasks;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignDetailViewModel : BaseViewModel
    {
        private readonly CampaignBasicInfoViewModel _campaignBasicInfoViewModel;
        private readonly CampaignReceipientsInfoViewModel _campaignReceipientsInfoViewModel;
        private readonly CampaignSendingInfoViewModel _campaignSendingInfoViewModel;

        public CampaignDetailViewModel(CampaignBasicInfoViewModel campaignBasicInfoViewModel,
                                       CampaignReceipientsInfoViewModel campaignReceipientsInfoViewModel,
                                       CampaignSendingInfoViewModel campaignSendingInfoViewModel)
        {
            PreviewCommand = new Command(async () => await ExecutePreviewCommand());
            _campaignBasicInfoViewModel = campaignBasicInfoViewModel;
            _campaignReceipientsInfoViewModel = campaignReceipientsInfoViewModel;
            _campaignSendingInfoViewModel = campaignSendingInfoViewModel;
            CampaignName = "Mock Campaign";
            CampaignType = "Classic Campaign";
        }

        public CampaignBasicInfoViewModel CampaignBasicInfoViewModel
        {
            get => _campaignBasicInfoViewModel;
        }

        public CampaignReceipientsInfoViewModel CampaignReceipientsInfoViewModel
        {
            get => _campaignReceipientsInfoViewModel;
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

        public override async Task InitializeAsync(object navigationData)
        {
        }

        public async Task ExecutePreviewCommand()
        {
            //TODO: - do something to show the preview
        }
    }
}
