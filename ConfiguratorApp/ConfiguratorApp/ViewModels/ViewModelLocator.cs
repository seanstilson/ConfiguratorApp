using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfiguratorApp.ViewModels
{
   public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Bootstrap.Initialize();
        }

        public BasePageViewModel BasePageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BasePageViewModel>();
            }
        }

        public MainPageViewModel MainPageVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainPageViewModel>();
            }
        }

        public SpreadsheetViewViewModel SpreadSheetVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpreadsheetViewViewModel>();
            }
        }

        public ErrorPageViewModel ErrorVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ErrorPageViewModel>();
            }
        }

    }
}
