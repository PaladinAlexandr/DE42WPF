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
        Product thisProduct;
        public EditProductWindow(Product product)
        {
            thisProduct = product;
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
        public string Validate()
        {
            string result = "";
            if (double.TryParse(PriceTextBox.Text, out double price) == false)
            {
                result += "\nЦена должна быть числом";
            }
            else if (price < 0)
            {
                result += "\nЦена должна быть положительной";
            }
            if (int.TryParse(AmountTextBox.Text, out int amount) == false)
            {
                result += "\nКоличество должно быть числом";
            }
            else if (amount < 0)
            {
                result += "\nКоличество должно быть положительным";
            }

            return result;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string result = Validate();
            if (result != "")
            {
                MessageBox.Show(result);
                return;
            }

            var DB = new PaladinDe42Context();
            var currentProduct = DB.Products.Where(x => x.Id == thisProduct.Id).FirstOrDefault();
            if (currentProduct == null) return;

            currentProduct.Name = NameTextBox.Text;
            currentProduct.Article = ArticleTextBox.Text;
            currentProduct.Category = CategoryTextBox.Text;
            currentProduct.Description = DescriptionTextBox.Text;
            currentProduct.Discount = double.Parse(DiscountTextBox.Text);
            currentProduct.Amount = int.Parse(AmountTextBox.Text);
            currentProduct.Price = decimal.Parse(PriceTextBox.Text);
            currentProduct.Supplier = SupplierTextBox.SelectedIndex + 1;
            currentProduct.Manufacture = ManufactureTextBox.SelectedIndex + 1;
            currentProduct.UnitMetric = UnitMetricTextBox.Text;


            DB.SaveChanges();
            MessageBox.Show("Продукт успешно отредактирован");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ListProductWindow().Show();
            Close();
        }
    }
}
