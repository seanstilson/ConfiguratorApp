using ConfiguratorApp.Models;
using ConfiguratorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConfiguratorApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel ViewModel => BindingContext as MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Product p = e.Item as Product;
            ViewModel.SetProductRevisionsVisible(p);
            BigList.ItemsSource = ViewModel.Products;

        }

        private void BigList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Product p = e.SelectedItem as Product;
            try
            {
                Device.InvokeOnMainThreadAsync(() =>
                {
                    ViewModel.SetProductRevisionsVisible(p);
                });
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
           
           // BigList.ItemsSource = ViewModel.Products;
        }

        private void ListView_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            Device.InvokeOnMainThreadAsync(() =>
           {
               ViewModel.SelectedRevision = e.Item as Revision;
           });
            
        }
    }
}
