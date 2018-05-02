using System;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Services;
using Doppler.Mobile.Navigation;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignPreviewViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ICampaignService _campaignService;

        public CampaignPreviewViewModel(INavigationService navigationService,
                                        ICampaignService campaignService)
        {
            _campaignService = campaignService;
            _navigationService = navigationService;
        }

        private HtmlWebViewSource _htmlSource;
        public HtmlWebViewSource HtmlSource
        {
            get { return _htmlSource; }
            set { SetProperty(ref _htmlSource, value); }
        }
        private string _currentErrorMsg;
        public string CurrentErrorMsg
        {
            get { return _currentErrorMsg; }
            set { SetProperty(ref _currentErrorMsg, value); }
        }

        private bool _hasNotPreview;
        public bool HasNotPreview
        {
            get { return _hasNotPreview; }
            set
            {
                SetProperty(ref _hasNotPreview, value);
                OnPropertyChanged("ShowMsgError");
                OnPropertyChanged("ShowHTML");
            }
        }

        public bool ShowMsgError
        {
            get => HasNotPreview;
        }

        public bool ShowHTML
        {
            get => !HasNotPreview;
        }


        public override async Task InitializeAsync(object navigationData)
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                var getPreviewResponse = await _campaignService.GetCampaignHtmlPreviewAsync(currentCampaign);
                if (getPreviewResponse.IsSuccessResult)
                {
                    var htmlSource = new HtmlWebViewSource();
                    htmlSource.Html = getPreviewResponse.SuccessValue;
                    HtmlSource = htmlSource;
                    HasNotPreview = false;
                }
                else
                {
                    CurrentErrorMsg = getPreviewResponse.ErrorValue;
                    HasNotPreview = true;
                }
            }
        }
    }
}
