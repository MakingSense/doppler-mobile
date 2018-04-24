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
    public class CampaignRecipientsInfoViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ICampaignRecipientService _campaignRecipientService;

        public CampaignRecipientsInfoViewModel(ICampaignRecipientService campaignRecipientService, INavigationService navigationService)
        {
            ListOfCampaignRecipient = new ObservableCollection<CampaignRecipient>();
            _navigationService = navigationService;
            _campaignRecipientService = campaignRecipientService;
            InitializeAsync();
        }

        public ObservableCollection<CampaignRecipient> ListOfCampaignRecipient { get; set; }

        private async void InitializeAsync()
        {
            var currentCampaign = _navigationService.CurrentCampaign;
            if (currentCampaign != null)
            {
                await GetCampaignRecipients(currentCampaign.CampaignId);
            }
        }

        private async Task GetCampaignRecipients(int campaignId)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var campaignRecipientServiceResponse = await _campaignRecipientService.FetchCampaignRecipientsAsync(campaignId);

            if (!campaignRecipientServiceResponse.IsSuccessResult)
                OnFetchCampaignRecipientFailed(campaignRecipientServiceResponse.ErrorValue);

            OnFetchCampaignRecipientSuccess(campaignRecipientServiceResponse.SuccessValue);
            IsBusy = false;
        }

        private void OnFetchCampaignRecipientFailed(string msg)
        {
            Application.Current.MainPage.DisplayAlert("", msg, "OK");
        }

        private void OnFetchCampaignRecipientSuccess(IList<CampaignRecipient> campaignRecipients)
        {
            campaignRecipients.ToList().ForEach(ListOfCampaignRecipient.Add);
        }
    }
}
