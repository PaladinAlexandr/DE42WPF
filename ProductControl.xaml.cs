using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace DE42WPF
{
    /// <summary>
    /// Логика взаимодействия для ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public Product thisProduct;
        public ProductControl(Product product)
        {
            thisProduct = product;
            InitializeComponent();
            NameCategoryTextBlock.Text = $"{product.Category}|{product.Name}";
            DescriptionTextBlock.Text = $"{product.Description}";
            SupplierTextBlock.Text = $"{product.SupplierNavigation.NameSupplier}";
            ManufactureTextBlock.Text = $"{product.ManufactureNavigation.NameManufacture}";
            UnitMetricTextBlock.Text = $"{product.UnitMetric}";
            AmountTextBlock.Text = $"{product.Amount}";
            DiscountTextBlock.Text = $"{product.Discount}";

            if (product.Discount > 0)
            {
                OldPrice.TextDecorations = TextDecorations.Strikethrough;
                OldPrice.Text = $"{product.Price}";
                OldPrice.Foreground = Brushes.Red;
                var price = (decimal)(((100m - (decimal)product.Discount) / 100m) * product.Price);
                NewPrice.Text = price.ToString("F2");
            }
            //((100-DISCOUNT) /100) * PRICE;

            if (product.Discount > 15)
            {
                this.Background = Brushes.SeaGreen;
            }
            if (product.Photo != null)
            {
                PhotoProductImage.Source = new BitmapImage(
                    new Uri($"C:\\Users\\1\\Documents\\GitHub\\DE42WPF\\DE42WPF\\Resources\\{product.Photo}"));
            }
        }
    }
}
