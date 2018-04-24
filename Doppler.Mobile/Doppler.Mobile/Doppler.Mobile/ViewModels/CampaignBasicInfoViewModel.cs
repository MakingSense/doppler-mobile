using System.Threading.Tasks;
using Doppler.Mobile.Navigation;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignBasicInfoViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public CampaignBasicInfoViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Initialize();
        }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Preheader { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }

        private void Initialize()
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                Name = currentCampaign.Name;
                Subject = currentCampaign.Subject;
                Preheader = currentCampaign.Preheader;
                FromName = currentCampaign.FromName;
                FromEmail = currentCampaign.FromEmail;
            }
        }
    }
}
