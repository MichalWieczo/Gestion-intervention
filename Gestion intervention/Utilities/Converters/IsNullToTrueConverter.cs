using System.Globalization;
using Microsoft.Maui.Controls;

namespace Gestion_intervention.Utilities.Converters
{
    // Renvoie true si la valeur est null (utile pour START)
    public class IsNullToTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value == null;
        public object ConvertBack(object v, Type t, object p, CultureInfo c) => throw new NotImplementedException();
    }
}
