using System.Threading.Tasks;
using Doppler.Mobile.Core.Models;
using Doppler.Mobile.ViewModels;

namespace Doppler.Mobile.Navigation
{
    public interface INavigationService
    {
        BaseViewModel PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateInNewStackToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Campaign CurrentCampaign { get; set; }
    }
}
