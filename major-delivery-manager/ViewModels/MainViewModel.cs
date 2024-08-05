using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;

        public MainViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            mainCodeBehind = codeBehind;
        }
    }
}
