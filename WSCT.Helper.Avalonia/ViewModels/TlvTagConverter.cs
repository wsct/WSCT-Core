using System;
using System.Globalization;
using Avalonia.Data.Converters;
using WSCT.Helpers.BasicEncodingRules;

namespace WSCT.Helper.Avalonia.ViewModels
{
    public class TlvTagConverter : IValueConverter
    {
        #region >> IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                TlvData tlv => tlv.ToString(parameter as string, null),
                _ => "unknown"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}