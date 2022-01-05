using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DialogValidation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ProductItems productItems;

        public MainWindow()
        {
            InitializeComponent();

            // Create some ProductItems and show in DataGrid
            productItems = CreateData(8);
            ProductsDataGrid.ItemsSource = productItems;
        }

        /// <summary>
        /// Create example product data.
        /// </summary>
        /// <param name="length">Numeber of products</param>
        /// <returns>ProductItems collection of ProductItem.</returns>
        static ProductItems CreateData(int length)
        {
            Random random = new();
            double price;
            ProductItems products = new();

            for (int i = 0; i < length; i++)
            {
                price = Math.Round(random.NextDouble() * 100, 2); // Random price 0 to 100.

                ProductItem item = new()
                {
                    ProductName = "Product " + i,
                    ProductDescription = "Description " + i,
                    ProductPrice = price
                };

                products.Add(item);
            }

            return products;
        }

        private void ProductsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            if (ProductsDataGrid.SelectedItem is ProductItem productItem)
            { // SelectedItem is of type ProductItem and not null!
                Debug.WriteLine(string.Format("Product selected = {0}", productItem.ProductName));

                ProductItem copy = new()
                {
                    ProductName = productItem.ProductName,
                    ProductDescription = productItem.ProductDescription,
                    ProductPrice = productItem.ProductPrice
                };

                ProductDialog dialog = new(productItems, productItem.ProductName)
                {
                    DataContext = copy
                };

                if(dialog.ShowDialog() is true)
                {
                    productItem.ProductName = copy.ProductName;
                    productItem.ProductDescription = copy.ProductDescription;
                    productItem.ProductPrice = copy.ProductPrice;
                }

                ProductsDataGrid.SelectedIndex = -1; // Clear DataGrid selection, so the same selected item can raise selection changed again if required!
            }
        }

        private void AddNewButton_Click(object sender, RoutedEventArgs e)
        {
            ProductItem copy = new();

            ProductDialog dialog = new(productItems, "")
            {
                DataContext = copy
            };

            if (dialog.ShowDialog() is true)
            {
                productItems.Add(copy);
            }            
        }
    }
}
