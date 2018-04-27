using System;
using System.Globalization;
using Xamarin.Forms;

namespace Doppler.Mobile.Converters
{
    /// <summary> A type converter for bool and int values. </summary>
    public class IntZeroToBoolConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            bool visibility = (int)value != 0;
            return visibility;
        }

        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
