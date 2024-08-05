using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager
{
    public interface IMainWindowsCodeBehind
    {
        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        /// <param name="message">Текст сообщения</param>

        void ShowMessage(string message);

        /// <summary>
        /// Загрузка View
        /// </summary>
        /// <param name="view"></param>

        void LoadView(ViewType typeView);
    }
}
