using ConfiguratorApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfiguratorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new SpreadsheetView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
