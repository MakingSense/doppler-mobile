namespace Doppler.Mobile.ViewModels
{
    public class CampaignBasicInfoViewModel : BaseViewModel
    {
        public CampaignBasicInfoViewModel()
        {
            Name = "February Newsletter";
            Subject = "March comes with surprises";
            Preheader = "Lorem";
            FromName = "John Doe";
            FromEmail = "Test@Mock.com";
        }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Preheader { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }
    }
}
