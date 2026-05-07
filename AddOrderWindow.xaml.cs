using Microsoft.EntityFrameworkCore.Infrastructure;
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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public AddOrderWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = Validate();
            if (result != "")
            {
                MessageBox.Show(result);
                return;
            }
            Order newOrder = new Order();
            var DB = new PaladinDe42Context();

            var pickPoint = AddressPickPointTextBox.Text.Split(',');
            newOrder.Address = DB.PickPoints.Where(x => x.IndexCity == int.Parse(pickPoint[0]))
                .Select(x => x.Id).FirstOrDefault();

            newOrder.Status = StatusOrderTextBox.SelectedIndex == 0 ? "Завершён" : "Новый";
            newOrder.DateOrder = DateTime.Parse(DateOrderTextBox.Text);
            newOrder.DateDelivery = DateTime.Parse(DateDeliveryTextBox.Text);
            newOrder.Id = DB.Orders.Max(x => x.Id) + 1;
            var product = DB.Products.Where(x => x.Article == ArticleOrderTextBox.Text).FirstOrDefault();

            OrderProduct orderProduct = new OrderProduct();
            orderProduct.NumberOrder = newOrder.Id;
            orderProduct.Amount = int.Parse(AmountOrderTextBox.Text);
            orderProduct.Product = product.Id;
            orderProduct.NumberOrderNavigation = newOrder;
            orderProduct.ProductNavigation = product;

            DB.Orders.Add(newOrder);
            DB.SaveChanges();
            var DB2 = new PaladinDe42Context();
            DB2.OrderProducts.Add(orderProduct);
            DB2.SaveChanges();
            MessageBox.Show("Новый заказ успешно сохранён");
            new ListOrderWindow().Show();
            Close();
        }
        public string Validate()
        {
            string result = "";
            if (!DateOnly.TryParse(DateDeliveryTextBox.Text, out var dateDelivery))
            {
                result += "\nДата должна быть в формате dd.mm.yyyy";
            }
            if (!DateOnly.TryParse(DateOrderTextBox.Text, out var dateOrder))
            {
                result += "\nДата должна быть в формате dd.mm.yyyy";
            }
            var DB = new PaladinDe42Context();
            var product = DB.Products.Where(x => x.Article == ArticleOrderTextBox.Text).FirstOrDefault();
            if (product == null)
            {
                result += "\nТовара с указанным артикулом не существует";
            }
            var pickPoint = AddressPickPointTextBox.Text.Split(',');
            if (pickPoint.Length == null || pickPoint.Length != 4)
            {
                result += "\nАдрес должен быть в формате Индекс, город, улица, дом";
            }
            else if (!int.TryParse(pickPoint[0], out var INDEX))
            {

            }
            else if (null == DB.PickPoints.Where(x => x.IndexCity == int.Parse(pickPoint[0])))
            {
                result += "\n Указанного адреса не существует";
            }

            return result;
        }
    }
}
