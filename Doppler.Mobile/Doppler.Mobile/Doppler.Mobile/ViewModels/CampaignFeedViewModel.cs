using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Xamarin.Forms;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignFeedViewModel: BaseViewModel
    {
        public CampaignFeedViewModel()
        {
            Title = "Campaigns";
            Campaigns = new ObservableCollection<Campaign>();
            LoadCampaignsCommand = new Command(async () => await ExecuteLoadCampaignsCommand());
            LogoutCommand = new Command(async () => await ExecuteLogoutCommand());
        }

        public ObservableCollection<Campaign> Campaigns { get; set; }

        public Command LoadCampaignsCommand { get; set; }

        public Command LogoutCommand { get; set; }

        public override async Task InitializeAsync(object navigationData)
        {
            await ExecuteLoadCampaignsCommand();
        }

        public async Task ExecuteLogoutCommand()
        {
            
        }

        public async Task ExecuteLoadCampaignsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Campaigns.Clear();
                var campaigns = await GetCampaignMock();
                foreach (var campaign in campaigns)
                {
                    Campaigns.Add(campaign);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IEnumerable<Campaign>> GetCampaignMock()
        {
            var mockCampaigns = new List<Campaign>
            {
                //TODO: complete with real example of campaigns
                new Campaign { CampaignId = Guid.NewGuid().ToString(),
                    //ScheduledDate = DateTimeOffset.Now,
                    RecipientsRequired= false,
                    ContentRequired = false,
                    Name = "Test2",
                    FromName = "",
                    FromEmail = "",
                    Subject = "",
                    Preheader = "",
                    ReplyTo = "",
                    TextCampaign = false,
                    Status = "Draft"
                },

                new Campaign { CampaignId = Guid.NewGuid().ToString(),
                    //ScheduledDate = DateTimeOffset.Now,
                    RecipientsRequired= false,
                    ContentRequired = false,
                    Name = "Test",
                    FromName = "",
                    FromEmail = "",
                    Subject = "",
                    Preheader = "",
                    ReplyTo = "",
                    TextCampaign = false,
                    Status = "Draft"
                },
                new Campaign { CampaignId = Guid.NewGuid().ToString(),
                    //ScheduledDate = DateTimeOffset.Now,
                    RecipientsRequired= false,
                    ContentRequired = false,
                    Name = "Test3",
                    FromName = "",
                    FromEmail = "",
                    Subject = "",
                    Preheader = "",
                    ReplyTo = "",
                    TextCampaign = false,
                    Status = "Draft"
                }
            };

            return await Task.FromResult(mockCampaigns);
        }
    }
}
