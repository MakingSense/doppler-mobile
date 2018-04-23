using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Doppler.Mobile.Core.Models;

namespace Doppler.Mobile.ViewModels
{
    public class CampaignReceipientsInfoViewModel : BaseViewModel
    {
        public CampaignReceipientsInfoViewModel()
        {
            ListOfSubscribers = new ObservableCollection<SubscriberList>();
            var listMock = CreateListOfSubscribersMock();
            listMock.ToList().ForEach(ListOfSubscribers.Add);
        }

        public ObservableCollection<SubscriberList> ListOfSubscribers { get; set; }

        private List<SubscriberList> CreateListOfSubscribersMock()
        {
            var list1 = new SubscriberList
            {
                Name = "Graphic Designers",
                NumberOfSubscribers = 230
            };

            var list2 = new SubscriberList
            {
                Name = "Doctors",
                NumberOfSubscribers = 5530
            };

            return new List<SubscriberList> { list1, list2 };
        }
    }
}
