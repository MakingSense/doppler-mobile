namespace Doppler.Mobile.ViewModels
{
    public class CampaignSendingInfoViewModel
    {
        public CampaignSendingInfoViewModel()
        {
            ConfirmationEmail = "mock@data.com";
            Date = "08/28/2017";
            Hour = "05:26 PM - (GMT-3) Bs As";
        }

        public string Date { get; set; }

        public string Hour { get; set; }

        public string ConfirmationEmail { get; set; }
    }
}
