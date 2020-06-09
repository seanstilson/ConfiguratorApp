using ConfiguratorApp.Models;
using ConfiguratorApp.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataGrid;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Xamarin.Forms.Color;

namespace ConfiguratorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpreadsheetView : ContentPage
    {

        public List<ExcelProduct> ProductList { get; set; }

        public SpreadsheetViewViewModel ViewModel => this.BindingContext as SpreadsheetViewViewModel;

        public SpreadsheetView()
        {
            
            ProductList = new List<ExcelProduct>();
            InitializeComponent();
            string fileName = @"Assets\ConfiguredOptionsReport.xlsx";

            MessagingCenter.Subscribe<SpreadsheetViewViewModel>(this, "Reload", async (arg) =>
            {
                await Device.InvokeOnMainThreadAsync(async () =>
                 {
                     
                     DataGrid.ItemsSource = ViewModel.CurrentProducts;
                     await Scroller.ScrollToAsync(0, 0, true);
                 });
            });

            try
            {
                //ReadExcelIntoObjects(fileName, null);
                DataGrid.ItemsSource = ViewModel.CurrentProducts;
                DataGrid.UserEditMode = Telerik.XamarinForms.DataGrid.DataGridUserEditMode.Cell;
                DataGrid.AlternateRowBackgroundStyle = new DataGridBorderStyle { BackgroundColor = Color.FromHex("#ebeef2") };
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            
        }

        public void ReadExcelTheRightWay(string fileName, string productNum)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var firstSheet = package.Workbook.Worksheets["Configured Options"];
                int row = 1, j = 1;
                for (j = 1; j < firstSheet.Dimension.Columns; j++)                   
                {
                    if (row > 1 && firstSheet.Cells[row, 2].Text != productNum)
                        break;

                    for (row = 1; row < 100 /*firstSheet.Dimension.Rows*/; row++)
                    {
                        if (row == 1)
                        {
                            /*Spreader.Children.Add(new Label
                            {
                                Text = firstSheet.Cells[row, j].Text,
                                FontFamily = "Ariel",
                                FontSize = 12,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = System.Drawing.Color.DarkGreen,
                                TextColor = Color.White
                            }, j - 1, j, row - 1, row); */
                        }
                        else 
                        {  
                           var enterHere =
                            new Entry
                            {
                                Text = firstSheet.Cells[row, j].Text,
                                FontFamily = "Ariel",
                                FontSize = 12,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                TextColor = Color.Black
                            };
                            //Spreader.Children.Add(enterHere, j - 1, j, row - 1, row);

                           
                        }
                    }
                }
                Console.WriteLine("Sheet 1 Data");
               
              //  Console.WriteLine($"Cell A2 Value   : {secondSheet.Cells["A2"].Text}"); */
            }
        }

        public void ReadExcelIntoObjects(string fileName, string productNum)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var firstSheet = package.Workbook.Worksheets["Configured Options"];
                int row = 1, j = 1;
                for (row = 1; row < 5000; row++)
                {
                    //if (row > 1 && firstSheet.Cells[row, 2].Text != productNum)
                    //    break;

                    ExcelProduct ep = new ExcelProduct();
                    ep.ProductName = firstSheet.Cells[row, 1].Text;
                    ep.ProductNumber = firstSheet.Cells[row, 2].Text;
                    ep.FeatureName = firstSheet.Cells[row, 3].Text;
                    ep.FeatureRequired = firstSheet.Cells[row, 4].Text;
                    ep.FeatureCSROnly = firstSheet.Cells[row, 5].Text;
                    ep.OptionGroupName = firstSheet.Cells[row, 6].Text;
                    ep.OptionGroupRequired = firstSheet.Cells[row, 7].Text;
                    ep.OptionGroupCSRonly = firstSheet.Cells[row, 8].Text;
                    ep.SubOptionGroupName = firstSheet.Cells[row, 9].Text;
                    ep.SubOptionGroupRequired = firstSheet.Cells[row, 10].Text;
                    ep.OptionName = firstSheet.Cells[row, 11].Text;
                    ep.OptionCode = firstSheet.Cells[row, 12].Text;
                    ep.HCPCS = firstSheet.Cells[row, 13].Text;
                    ep.OptionRequired = firstSheet.Cells[row, 14].Text;
                    ep.OptionCSROnly = firstSheet.Cells[row, 15].Text;
                    ep.WorkTicketInput = firstSheet.Cells[row, 16].Text;
                    ProductList.Add(ep);
                    //  for (j = 1; j < firstSheet.Dimension.Columns; j++)
                    // {

                    //  }
                }
                Console.WriteLine("Sheet 1 Data");
                
            }
        }

        static void ReadExcelFileSAX(string fileName)
        {
            using (SpreadsheetDocument spreadsheet = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadsheet.WorkbookPart;
                WorksheetPart worksheet = workbookPart.WorksheetParts.First();

                OpenXmlReader reader = OpenXmlReader.Create(worksheet);

                string text;
                while (reader.Read())
                {
                    if ( reader.ElementType == typeof(CellValue))
                    {
                        text = reader.GetText();
                        Console.WriteLine(text);
                    }
                }
            }
        }

        static void ReadExcelFileDOM(string fileName)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fileName, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                string text;
                foreach (Row r in sheetData.Elements<Row>())
                {
                    foreach (DocumentFormat.OpenXml.Spreadsheet.Cell c in r.Elements<DocumentFormat.OpenXml.Spreadsheet.Cell>())
                    {
                        text = c.InnerText;
                        Console.Write(text + " ");
                    }
                }
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var searchVal = EntryCrank.Text;
            var searchFor = Searcher.SelectedItem;
            ViewModel.Searching = true;
            ViewModel.GlobalSeach();
            Device.BeginInvokeOnMainThread(() =>
            {
                DataGrid.ItemsSource = ViewModel.CurrentProducts;
            });
        }

        private void Scroller_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (ViewModel.Searching)
                return;

            var scroller = sender as ScrollView;
            var pos = e.ScrollY;
            if (pos >= scroller.ContentSize.Height - 1200)
            {
                ViewModel.GetNextPage();
                Device.BeginInvokeOnMainThread(() =>
                {
                    DataGrid.ItemsSource = ViewModel.CurrentProducts;
                    scroller.ScrollToAsync(0, 100, true);
                });
            }
            else if (pos == 0)
            {
                ViewModel.GetPreviousPage();
                Device.BeginInvokeOnMainThread(() =>
                {
                    DataGrid.ItemsSource = ViewModel.CurrentProducts;
                    if (ViewModel.CurrentIndex > 1)
                        scroller.ScrollToAsync(0, 100, true);
                });

            }
                
            
        }

        private void DataGrid_SelectionChanged(object sender, DataGridSelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count() > 0)
            {
                ViewModel.SelectedItem = e.AddedItems.First() as ExcelProduct;
                if ( ViewModel.Editing )
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        DataGrid.ItemsSource = ViewModel.CurrentProducts;
                        //Scroller.ScrollToAsync(0, 100, true);
                    });
            }
                
        }

        private void DataGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private void DataGrid_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void EntryCrank_TextChanged(object sender, TextChangedEventArgs e)
        {
            var edit = sender as Entry;
            if ( string.IsNullOrEmpty(edit.Text))
            {
                ViewModel.ReloadGrid();
                Device.BeginInvokeOnMainThread(() =>
                {
                    DataGrid.ItemsSource = ViewModel.CurrentProducts;
                    ViewModel.Searching = false;
                    //Scroller.ScrollToAsync(0, 100, true);
                });
            }
        }
    }
}