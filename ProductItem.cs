using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DialogValidation
{
    public class ProductItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal struct Item
        {
            internal string ProductName { get; set; }
            internal string ProductDescription { get; set; }
            internal double ProductPrice { get; set; }            
        }

        Item current;

        public string ProductName
        {
            get { return current.ProductName; }
            set
            {
                if(current.ProductName != value)
                {
                    current.ProductName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ProductDescription
        {
            get { return current.ProductDescription; }
            set
            {
                if (current.ProductDescription != value)
                {
                    current.ProductDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public double ProductPrice
        {
            get { return current.ProductPrice; }
            set
            {
                if(current.ProductPrice != value)
                {
                    current.ProductPrice = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    public class ProductItems : ObservableCollection<ProductItem>
    {
        public ProductItems() { }
    }
}
