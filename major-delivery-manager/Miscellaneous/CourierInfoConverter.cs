using major_delivery_manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace major_delivery_manager.Miscellaneous
{
    internal class CourierInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var info = (CourierModel)value;
            switch (info)
            {
                case null:
                    return "Курьер не назначен";
                default:
                    return ("Курьер: " + info.FullName);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
