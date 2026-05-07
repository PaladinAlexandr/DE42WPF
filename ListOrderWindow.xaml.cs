using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ListOrderWindow.xaml
    /// </summary>
    public partial class ListOrderWindow : Window
    {
        public ListOrderWindow()
        {
            InitializeComponent();

            var DB = new PaladinDe42Context();
            var orders = DB.Orders.Include(x=>x.AddressNavigation);
            foreach (var order in orders)
            {
                OrderListBox.Items.Add(new OrderControl(order));
            }

        }

        private void OrderListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            new ListProductWindow().Show();
            Close();
        }
    }
}
