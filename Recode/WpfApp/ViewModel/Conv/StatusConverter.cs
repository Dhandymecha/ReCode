using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApp.ViewModel.Conv
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) {
                return DependencyProperty.UnsetValue;
            }

            SolidColorBrush brush = new SolidColorBrush(Colors.White);

            bool status = (bool)value;
            if (status) {
                brush = new SolidColorBrush(Colors.ForestGreen);
            }
            else {
                brush = new SolidColorBrush(Colors.Red);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
