using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace ClientApp.Model
{
    public class Product : INotifyPropertyChanged
    {
        private string _name;
        private string _description;

        public static Product CreateProduct(Guid id, string name, string description, BitmapImage iconImage, List<BitmapImage> images)
        {
            return new Product()
            {
                Id = id,
                _name=  name,
                _description = description,
                IconImage = iconImage,
                Images = images != null? new ObservableCollection<BitmapImage>(images) : new ObservableCollection<BitmapImage>()
            };
        }

        public Guid Id { get; private set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public BitmapImage IconImage { get; private set; }

        public ObservableCollection<BitmapImage> Images { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
