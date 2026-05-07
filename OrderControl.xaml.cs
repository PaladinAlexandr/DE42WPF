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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DE42WPF
{
    /// <summary>
    /// Логика взаимодействия для OrderControl.xaml
    /// </summary>
    public partial class OrderControl : UserControl
    {
        public Order order;
        public OrderControl(Order order)
        {
            this.order = order;
            InitializeComponent();

            DateDeliveryTextBlock.Text = $"{order.DateDelivery.Value.ToString("yyyy.MM.dd")}";
            DateOrderTextBlock.Text = $"{order.DateOrder.Value.ToShortDateString()}";
            StatusOrderTextBlock.Text = $"{order.Status}";

            var DB = new PaladinDe42Context();
            var orderProducts = DB.OrderProducts.Where(x => x.NumberOrder == order.Id).Include(x => x.ProductNavigation);
            string article = "";
            foreach (var item in orderProducts)
            {
                article += item.ProductNavigation.Article + ", ";
                article += item.Amount + ",";
            }
            article = article.TrimEnd(',');


            ArticleOrderTextBlock.Text = article;
            var address = order.AddressNavigation;
            AddressPickPointTextBlock.Text = $"{address.IndexCity},{address.City},{address.Street},{address.Home}";
        }
    }
}
