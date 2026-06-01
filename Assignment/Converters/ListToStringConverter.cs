using System.Globalization;

namespace Assignment.Converters;

public class ListToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is List<string> list && list.Count > 0)
            return "• " + string.Join("\n• ", list);
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}