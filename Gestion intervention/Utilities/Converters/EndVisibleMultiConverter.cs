using System.Globalization;
using Microsoft.Maui.Controls;

namespace Gestion_intervention.Utilities.Converters
{
    // Renvoie true si StartTime != null ET EndTime == null (utile pour END)
    public class EndVisibleMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var start = values.Length > 0 ? values[0] : null;
            var end = values.Length > 1 ? values[1] : null;
            return start != null && end == null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
