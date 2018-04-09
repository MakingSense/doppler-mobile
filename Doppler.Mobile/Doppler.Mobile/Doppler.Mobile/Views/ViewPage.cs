using Autofac;
using Doppler.Mobile.DI;
using Doppler.Mobile.ViewModels;
using Xamarin.Forms;

namespace Doppler.Mobile.Views
{
    public class ViewPage<T> : ContentPage where T : BaseViewModel
    {
        readonly T _viewModel;
        public T ViewModel
        {
            get { return _viewModel; }
        }

        public ViewPage()
        {
            using (var scope = AppContainer.Container.BeginLifetimeScope())
            {
                _viewModel = AppContainer.Container.Resolve<T>();
            }
            BindingContext = _viewModel;
        }

        public void InitializeComponent() {
            InitializeComponent();
        }
    }
}
