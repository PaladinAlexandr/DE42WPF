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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
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

            var product = new Product();
            product.Name = NameTextBox.Text;
            product.Article = ArticleTextBox.Text;
            product.Category = CategoryTextBox.Text;
            product.Description = DescriptionTextBox.Text;
            product.Discount = double.Parse(DiscountTextBox.Text);
            product.Amount = int.Parse(AmountTextBox.Text);
            product.Price = decimal.Parse(PriceTextBox.Text);
            product.Supplier = SupplierTextBox.SelectedIndex + 1;
            product.Manufacture = ManufactureTextBox.SelectedIndex + 1;
            product.UnitMetric = UnitMetricTextBox.Text;
            product.Id = DB.Products.Max(x => x.Id) + 1;
            
            DB.Products.Add(product);
            DB.SaveChanges();
            MessageBox.Show("Добавлен новый товар");

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
            if (new PaladinDe42Context().Products.Where(x => x.Article == ArticleTextBox.Text).Any())
            {
                result += "\nАртикул должен быть уникальным. Такой артикул существует";

            }
            return result;
        }
    }
}
