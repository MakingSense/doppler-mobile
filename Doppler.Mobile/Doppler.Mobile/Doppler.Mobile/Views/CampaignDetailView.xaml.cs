using Doppler.Mobile.ViewModels;

namespace Doppler.Mobile.Views
{
    public partial class CampaignDetailView : ViewPage<CampaignDetailViewModel>
    {
        public CampaignDetailView()
        {
            InitializeComponent();
            campaignBasicInfoView.BindingContext = ViewModel.CampaignBasicInfoViewModel;
            campaignReceipientsInfoView.BindingContext = ViewModel.CampaignReceipientsInfoViewModel;
            campaignSendingInfoView.BindingContext = ViewModel.CampaignSendingInfoViewModel;
        }
    }
}
