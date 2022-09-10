using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClientApp.Model;
using ClientApp.Model.DataSuppliers;

namespace ClientApp.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly IProductSupplier _productSupplier;

        private Product _selectedProduct;

        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = _productSupplier.GetProductById(value.Id);

                OnPropertyChanged("SelectedProduct");
            }
        }

        public ApplicationViewModel(IProductSupplier productSupplier)
        {
            _productSupplier = productSupplier;

            Products = new ObservableCollection<Product>(_productSupplier.GetAllProducts());

            // set selected product
            if (Products.Count > 0)
                _selectedProduct = _productSupplier.GetProductById(Products[0].Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
