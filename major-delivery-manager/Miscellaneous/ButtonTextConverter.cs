using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace major_delivery_manager.Miscellaneous
{
    internal class ButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = value;
            switch (status)
            {
                case RequestState.NEW:
                    return "Начать";
                case RequestState.INPROCCESS:
                    return "Выполнить";
                default:
                    return "Обновить";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
