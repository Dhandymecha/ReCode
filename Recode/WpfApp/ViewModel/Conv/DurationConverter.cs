using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp.ViewModel.Conv
{
    public class DurationConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) {
                return DependencyProperty.UnsetValue;
            }
            DateTime start = (DateTime)values[0];
            DateTime end = (DateTime)values[1];

            var duration = end - start; 

            return duration.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
