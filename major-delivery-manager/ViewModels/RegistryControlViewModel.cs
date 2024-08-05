using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace major_delivery_manager.ViewModels
{
    class RegistryControlViewModel : BindableBase
    {
        private IMainWindowsCodeBehind mainCodeBehind;

        public RegistryControlViewModel(IMainWindowsCodeBehind codeBehind)
        {
            if (codeBehind == null) throw new ArgumentNullException(nameof(codeBehind));

            mainCodeBehind = codeBehind;
        }
    }
}
