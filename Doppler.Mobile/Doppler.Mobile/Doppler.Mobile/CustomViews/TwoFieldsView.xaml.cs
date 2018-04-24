using Xamarin.Forms;

namespace Doppler.Mobile.CustomViews
{
    public partial class TwoFieldsView : ContentView
    {
        public TwoFieldsView()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty NameTextProperty = BindableProperty.Create(
            propertyName: "NameText",
            returnType: typeof(string),
            declaringType: typeof(TwoFieldsView),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: NameTextPropertyChanged);

        public string NameText
        {
            get { return base.GetValue(NameTextProperty).ToString(); }
            set { base.SetValue(NameTextProperty, value); }
        }

        private static void NameTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TwoFieldsView)bindable;
            control.name.Text = newValue.ToString();
        }

        public static readonly BindableProperty ValueTextProperty = BindableProperty.Create(
            propertyName: "ValueText",
            returnType: typeof(string),
            declaringType: typeof(TwoFieldsView),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ValueTextPropertyChanged);

        public string ValueText
        {
            get { return base.GetValue(ValueTextProperty).ToString(); }
            set { base.SetValue(ValueTextProperty, value); }
        }

        private static void ValueTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TwoFieldsView)bindable;
            control.value.Text = newValue.ToString();
        }
    }
}
