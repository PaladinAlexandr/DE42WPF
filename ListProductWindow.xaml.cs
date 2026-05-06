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
    /// Логика взаимодействия для ListProductWindow.xaml
    /// </summary>
    public partial class ListProductWindow : Window
    {
        public ListProductWindow()
        {
            InitializeComponent();
            if (UserSingleton.GetUser != null)
            {
                FullnameTextBlock.Text = 
                    $" {UserSingleton.GetUser.Surname}" +
                    $" {UserSingleton.GetUser.Name}" +
                    $" {UserSingleton.GetUser.Patronymic}";
            }

            var DB = new PaladinDe42Context();
            var Products = DB.Products.Include(x => x.SupplierNavigation).Include(x => x.ManufactureNavigation);


            Products.ForEachAsync(item=> ProductListBox.Items.Add(new ProductControl(item)));

            //foreach (var item in Products)
            //{
            //    ProductListBox.Items.Add(new ProductControl(item));
            //}
        }
    }
}
