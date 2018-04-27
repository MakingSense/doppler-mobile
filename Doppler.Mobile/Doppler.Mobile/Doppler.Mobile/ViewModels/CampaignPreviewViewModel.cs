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

        private UriImageSource _imagePreview;
        public UriImageSource ImagePreview
        {
            get { return _imagePreview; }
            set { SetProperty(ref _imagePreview, value); }
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
            set { SetProperty(ref _hasNotPreview, value); }
        }

        public bool ShowMsgError
        {
            get => HasNotPreview;
        }

        public bool ShowImage
        {
            get => !HasNotPreview;
        }


        public override async Task InitializeAsync(object navigationData)
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                var getPreviewResponse = _campaignService.GetCampaignPreview(currentCampaign);
                if (getPreviewResponse.IsSuccessResult)
                {
                    ImagePreview = new UriImageSource
                    {
                        Uri = new Uri(getPreviewResponse.SuccessValue),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(5, 0, 0, 0)
                    };
                    HasNotPreview = false;
                }
                else
                {
                    CurrentErrorMsg = getPreviewResponse.ErrorValue;
                    HasNotPreview = false;
                }
            }
        }
    }
}
