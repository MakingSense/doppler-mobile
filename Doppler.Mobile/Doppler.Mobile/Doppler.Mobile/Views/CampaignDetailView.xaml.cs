using Doppler.Mobile.ViewModels;

namespace Doppler.Mobile.Views
{
    public partial class CampaignDetailView : ViewPage<CampaignDetailViewModel>
    {
        public CampaignDetailView()
        {
            InitializeComponent();
            campaignBasicInfoView.BindingContext = ViewModel.CampaignBasicInfoViewModel;
            campaignRecipientsInfoView.BindingContext = ViewModel.CampaignRecipientsInfoViewModel;
            campaignSendingInfoView.BindingContext = ViewModel.CampaignSendingInfoViewModel;
        }
    }
}
