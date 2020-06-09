using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfiguratorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExcelViewPage : ContentPage
    {
        public ExcelViewPage()
        {
            InitializeComponent();
            
        }

        public async Task GetFileContent(string fileName)
        {
            var fn = "ConfiguredOptionsReport.xlsx";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            //File.ReadAllText(file, "Hello World");

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new Xamarin.Essentials.ReadOnlyFile(file)
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await GetFileContent(string.Empty);
        }

        protected void WatchFileContent()
        {
           
        }
    }
}