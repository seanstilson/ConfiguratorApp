using ConfiguratorApp.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ConfiguratorApp.ViewModels
{
    public class SpreadsheetViewViewModel : BasePageViewModel
    {
        public List<ExcelProduct> AllProducts { get; set; }

        public List<ExcelProduct> CurrentProducts { get; set; }

        public bool Searching { get; set; }

        private FileSystemWatcher Watcher { get; set; }

        private bool ExcelIsOpened { get; set; }

        public bool Editing { get; set; }

        private ExcelProduct _selectedItem { get; set; }

        public ExcelProduct SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != _originalItem && _originalItem != null)
                {
                    if (Searching)
                    {
                        Editing = true;
                        UpdateEdit();
                        _originalItem = _selectedItem;
                    }
                }
                else
                    _originalItem = _selectedItem;   
                        
            }
        }

        private void CreateWatcher(string path)
        {
            //Create a new FileSystemWatcher.
            Watcher = new FileSystemWatcher();

            //Set the filter to all files.
            Watcher.Filter = "*.xlsx";

            Watcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.LastWrite | NotifyFilters.FileName;

            Watcher.InternalBufferSize = 64000;

            Watcher.Changed += new FileSystemEventHandler(Watcher_Changed);

            Watcher.Deleted += new FileSystemEventHandler(Watcher_Deleted);

            Watcher.Created += new FileSystemEventHandler(Watcher_Created);
           
            //Set the path 
            Watcher.Path = path;

            //Enable the FileSystemWatcher events.
            Watcher.EnableRaisingEvents = true;

        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            ReadExcelIntoObjects(FileName, string.Empty);
            ReloadGrid();
            MessagingCenter.Send<SpreadsheetViewViewModel>(this, "Reload");
        }

        /// <summary>
        /// This method is actually a little misleading in the way Visual studio performs the file management.
        /// the actual file changes occur to a temp file that gets created. VS creates a new file, then writes to the original, then deletes the temp.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            ReadExcelIntoObjects(FileName, string.Empty);
            ReloadGrid();
            MessagingCenter.Send<SpreadsheetViewViewModel>(this, "Reload");
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!ExcelIsOpened)
            {
                ExcelIsOpened = true;
               // Watcher.WaitForChanged(WatcherChangeTypes.Changed);
                return;
            }

            int ix = 0;

        }

        public async Task<bool> OpenExcel()
        {
            var fn = "ConfiguredOptionsReport.xlsx";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            //File.ReadAllText(file, "Hello World");

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new Xamarin.Essentials.ReadOnlyFile(file)
            });

            return true;
        }

        private ExcelProduct _originalItem { get; set; }

        public Double PageSize { get; set; }

        public Double CurrentIndex;

        public ICommand OpenExcelCommand => new Command(async () => { bool b = await OpenExcel(); });
        public SearchParam SelectedSearchParam { get; set; }

        public string SearchValue { get; set; }

        public SearchType SelectedSearchType {get; set;}
       
        public List<string> SearchParams => new List<string> { "Pick a search option", "Option Code", "Option Group", "Feature Name", "Option Name" };

        public List<string> SearchTypes => new List<string> { "Pick a search type", "Equals", "Is Not Equal To", "Starts With", "Contains", "Ends With" };

        private string _selectedParam;
        public string SelectedParam
        {
            get { return _selectedParam; }
            set { _selectedParam = value; SelectedSearchParam = SetSelectedParam(); }
        }

        private string _selectedSearchType;
        public string SelectedSearchTypeString
        {
            get { return _selectedSearchType; }
            set { _selectedSearchType = value; SelectedSearchType = SetSelectedSearchType(); }
        }

        private SearchParam SetSelectedParam()
        {
            switch (SelectedParam)
            {
                case "Option Group":
                    return SearchParam.OptionGroup;
                case "Feature Name":
                    return SearchParam.FeatureName;
                case "Option Name":
                    return SearchParam.OptionName;
                case "Option Code":
                    return SearchParam.OptionCode;
                default: return SearchParam.OptionCode;
            }
        }

        private SearchType SetSelectedSearchType()
        {
            switch (SelectedParam)
            {
                case "Equals":
                    return SearchType.Equals;
                case "Not Equal To":
                    return SearchType.NotEqualTo;
                case "Starts With":
                    return SearchType.StartsWith;
                case "Contains":
                    return SearchType.Contains;
                case "Ends With":
                    return SearchType.EndsWith;

                default: return SearchType.EndsWith;
            }
        }

        string fileName = @"ConfiguredOptionsReport.xlsx";
        string FileName => Path.Combine(FileSystem.CacheDirectory, fileName);

        public SpreadsheetViewViewModel()
        {
            Searching = false;
            
            PageSize = 2000;
            AllProducts = new List<ExcelProduct>();
            SelectedParam = SearchParams[0];
            SelectedSearchTypeString = SearchTypes[0];

            ReadExcelIntoObjects(FileName, string.Empty);
            CurrentIndex = PageSize;
            if (AllProducts.Count > PageSize)
                CurrentProducts = AllProducts.Take((int)PageSize).ToList();
            CreateWatcher(FileSystem.CacheDirectory);
        }

        public void ReloadGrid()
        {
            CurrentIndex = PageSize;
            if (AllProducts.Count > PageSize)
                CurrentProducts = AllProducts.Take((int)PageSize).ToList();
        }

        public void ReadExcelIntoObjects(string fileName, string productNum)
        {
            AllProducts.Clear();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                var firstSheet = package.Workbook.Worksheets["Configured Options"];
                int row = 1, j = 1;
                for (row = 1; row < firstSheet.Dimension.Rows; row++)
                {
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
                    AllProducts.Add(ep);                  
                }

            }
        }

        public void GetNextPage()
        {
            if (CurrentIndex >= AllProducts.Count)
                return;

            var diff = AllProducts.Count - CurrentIndex;
            if (diff > PageSize)
                diff = PageSize;

            CurrentProducts.Clear();
            CurrentProducts = AllProducts.GetRange((int)CurrentIndex, (int)diff);
            CurrentIndex += diff;
        }

        public void GetPreviousPage()
        {
            if (CurrentIndex <= 1 )
                return;

            var diff = CurrentIndex - PageSize;
            if (diff < 0)
                diff = CurrentIndex;
            else
                diff = PageSize;

            CurrentProducts.Clear();

            CurrentProducts = AllProducts.GetRange((int)(CurrentIndex - diff), (int)diff);

            CurrentIndex -= diff;
            
        }

        public void UpdateEdit()
        {
            var theProds = CurrentProducts;
            theProds.ForEach(tp => tp.FeatureName = _originalItem.FeatureName);
            CurrentProducts.Clear();
            CurrentProducts = theProds;
        }

        public void GlobalSeach()
        {
            CurrentProducts.Clear();
            CurrentProducts = PerformGlobalSearch(SelectedSearchParam, SelectedSearchType, SearchValue);
        }

        public List<ExcelProduct> PerformGlobalSearch(SearchParam searchParam, SearchType type, string value)
        {
            CurrentProducts.Clear();
            switch (type )
            {
                case SearchType.Equals:
                    return EqualsSearch(searchParam, value);
                case SearchType.StartsWith:
                    return BeginsSearch(searchParam, value);
                case SearchType.Contains:
                    return ContainsSearch(searchParam, value);
                case SearchType.EndsWith:
                    return EndsSearch(searchParam, value);
                case SearchType.NotEqualTo:
                    return NotEqualsSearch(searchParam, value);
                default:
                    return EndsSearch(searchParam, value);
            }
        }

        private List<ExcelProduct>BeginsSearch(SearchParam param, string value)
        {
            switch (param)
            {
                case SearchParam.FeatureName:
                    return AllProducts.Where(p => p.FeatureName.StartsWith(value)).ToList();
                case SearchParam.OptionCode:
                    return AllProducts.Where(p => p.OptionCode.StartsWith(value)).ToList();
                case SearchParam.OptionGroup:
                    return AllProducts.Where(p => p.OptionGroupName.StartsWith(value)).ToList();
                case SearchParam.OptionName:
                    return AllProducts.Where(p => p.OptionName.StartsWith(value)).ToList();
                default: return AllProducts.Where(p => p.OptionCode.StartsWith(value)).ToList();
            }
        }

        private List<ExcelProduct>EqualsSearch(SearchParam param, string value)
        {
            switch (param)
            {
                case SearchParam.FeatureName:
                    return AllProducts.Where(p => p.FeatureName == value).ToList();
                case SearchParam.OptionCode:
                    return AllProducts.Where(p => p.OptionCode == value).ToList();
                case SearchParam.OptionGroup:
                    return AllProducts.Where(p => p.OptionGroupName == value).ToList();
                case SearchParam.OptionName:
                    return AllProducts.Where(p => p.OptionName == value).ToList();
                default: return AllProducts.Where(p => p.OptionCode == value).ToList();
            }
        }

        private List<ExcelProduct> EndsSearch(SearchParam param, string value)
        {
            switch (param)
            {
                case SearchParam.FeatureName:
                    return AllProducts.Where(p => p.FeatureName.EndsWith(value)).ToList();
                case SearchParam.OptionCode:
                    return AllProducts.Where(p => p.OptionCode.EndsWith(value)).ToList();
                case SearchParam.OptionGroup:
                    return AllProducts.Where(p => p.OptionGroupName.EndsWith(value)).ToList();
                case SearchParam.OptionName:
                    return AllProducts.Where(p => p.OptionName.EndsWith(value)).ToList();
                default: return AllProducts.Where(p => p.OptionCode.EndsWith(value)).ToList();
            }
        }

        private List<ExcelProduct>ContainsSearch(SearchParam param, string value)
        {
            switch (param)
            {
                case SearchParam.FeatureName:
                    return AllProducts.Where(p => p.FeatureName.Contains(value)).ToList();
                case SearchParam.OptionCode:
                    return AllProducts.Where(p => p.OptionCode.Contains(value)).ToList();
                case SearchParam.OptionGroup:
                    return AllProducts.Where(p => p.OptionGroupName.Contains(value)).ToList();
                case SearchParam.OptionName:
                    return AllProducts.Where(p => p.OptionName.Contains(value)).ToList();
                default: return AllProducts.Where(p => p.OptionCode.Contains(value)).ToList();
            }
        }

        private List<ExcelProduct> NotEqualsSearch(SearchParam param, string value)
        {
            switch (param)
            {
                case SearchParam.FeatureName:
                    return AllProducts.Where(p => p.FeatureName != value).ToList();
                case SearchParam.OptionCode:
                    return AllProducts.Where(p => p.OptionCode != value).ToList();
                case SearchParam.OptionGroup:
                    return AllProducts.Where(p => p.OptionGroupName != value).ToList();
                case SearchParam.OptionName:
                    return AllProducts.Where(p => p.OptionName != value).ToList();
                default: return AllProducts.Where(p => p.OptionCode != value).ToList();
            }
        }
    }
}
