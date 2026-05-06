using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DE42WPF
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public EditProductWindow(Product product)
        {
            InitializeComponent();
            NameTextBox.Text = $"{product.Name}";
            CategoryTextBox.Text = $"{product.Category}";
            DescriptionTextBox.Text = $"{product.Description}";
            SupplierTextBox.SelectedIndex = (int)product.Supplier;
            ManufactureTextBox.SelectedIndex = (int)product.Manufacture;
            UnitMetricTextBox.Text = $"{product.UnitMetric}";
            AmountTextBox.Text = $"{product.Amount}";
            DiscountTextBox.Text = $"{product.Discount}";
            PriceTextBox.Text = $"{product.Price}";
            if (product.Photo != null)
            {
                PhotoProductImage.Source = new BitmapImage(
                    new Uri($"C:\\Users\\1\\Documents\\GitHub\\DE42WPF\\DE42WPF\\Resources\\{product.Photo}"));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
