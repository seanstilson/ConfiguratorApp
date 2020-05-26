using ConfiguratorApp.Models;
using ConfiguratorApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ConfiguratorApp.ViewModels
{
    public class MainPageViewModel : BasePageViewModel
    {
        private IProductService _productService;
        private List<Product> _products;
        private Product _selectedProduct;
        private Revision _selectedRevision;
        public string SelectedType { get; set; }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value;
                if (_selectedProduct != null)
                    SetProductRevisionsVisible(_selectedProduct); 
                OnPropertyChanged(nameof(SelectedProduct)); }
        }
        public Revision SelectedRevision
        {
            get { return _selectedRevision; }
            set { _selectedRevision = value;
                if (_selectedRevision != null){
                    var prod = _productService.GetProductByRevision(SelectedProduct.ID, _selectedRevision.Number);
                    if (prod != null)
                        {
                        Product[] pary = new Product[Products.Count];
                        Products.CopyTo(pary);
                        Products?.Clear();
                       // var prods = _productService.GetAllProducts();
                        Product p_rod = pary.SingleOrDefault(p => p.Name == prod.Name);
                        p_rod.LaunchDate = prod.LaunchDate;
                        p_rod.Revisions = prod.Revisions;
                        p_rod.CurrentRevisionNumber = prod.CurrentRevisionNumber;
                        Products = pary.ToList();
                        SelectedProduct = p_rod;
                        OnPropertyChanged(nameof(SelectedRevision));
                        }
                    }
                }
        }

        public List<Product> Products {
            get { return _products; }
            set { _products = value;  OnPropertyChanged(nameof(Products)); }
            }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public ICommand ButtonCommand => new Command(() => ResetProducts());

        public MainPageViewModel(IProductService productService) : base()
        {
            UserName = "Sean";
            WelcomeMessage = $"Welcome {UserName}";
            _productService = productService;
            Products = _productService.GetAllProducts();
            SelectedProduct = null;
        }

        public void SetProductRevisionsVisible(Product p)
        {
            string nm = p.Name;
            Products?.Clear();
            Products = _productService.GetAllProducts();
            Product prod = Products.SingleOrDefault(px => px.Name == nm);
            prod.ChildrenVisible = true;
        }

        public void ResetProducts()
        {
            Products?.Clear();
            Products = _productService.GetAllProducts();
            Products.First().ChildrenVisible = true;
        }


      }
}
