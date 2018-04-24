using System;
using System.Globalization;
using Doppler.Mobile.Navigation;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignSendingInfoViewModel
    {
        private readonly INavigationService _navigationService;

        public CampaignSendingInfoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Initialize();
        }

        public string Date { get; set; }

        public string Hour { get; set; }

        public string ConfirmationEmail { get; set; }

        private void Initialize()
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                ConfirmationEmail = currentCampaign.FromEmail;
                var localTime = currentCampaign.ScheduledDate.LocalDateTime;
                Hour = localTime.ToString("hh:mm tt - '(GMT'z')'");
                Date = localTime.ToString("MM/dd/yyyy");
            }
        }
    }
}
