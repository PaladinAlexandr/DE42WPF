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
using System.Windows.Markup;
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
        IQueryable<Product> Products;
        public ListProductWindow()
        {
            try
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
                Products = DB.Products.Include(x => x.SupplierNavigation).Include(x => x.ManufactureNavigation);


                Products.ForEachAsync(item => ProductListBox.Items.Add(new ProductControl(item)));


            }
            catch
            {

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortProduct();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortProduct();
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortProduct();
        }

        public void SortProduct()
        {
            //Сортировка по возрастанию/убыванию
            IQueryable<Product> newProducts = null;
            if (SortComboBox.SelectedIndex == 1)
                newProducts = Products.OrderByDescending(x => x.Amount);
            else
                newProducts = Products.OrderBy(x => x.Amount);
            //фильтрация по поставщику
            if (FilterComboBox.SelectedIndex == 1)
                newProducts = newProducts.Where(x => x.SupplierNavigation.NameSupplier == "Kari");
            else if (FilterComboBox.SelectedIndex == 2)
                newProducts = newProducts.Where(x => x.SupplierNavigation.NameSupplier == "Обувь для вас");
            //Поиск по всем текстовым атрибутам одновременно 
            string search = SearchTextBox.Text;
            if (search == "")
            {
                ProductListBox.Items.Clear();
                newProducts.ForEachAsync(item => ProductListBox.Items.Add(new ProductControl(item)));
                return;
            }
            newProducts = newProducts.Where(x => x.Article.Contains(search)
            || x.Name.Contains(search)
            || x.SupplierNavigation.NameSupplier.Contains(search)
            || x.ManufactureNavigation.NameManufacture.Contains(search)
            || x.Category.Contains(search)
            );

            if (newProducts == null)
            {
                ProductListBox.Items.Clear();
                return;
            }
            ProductListBox.Items.Clear();
            newProducts.ForEachAsync(item => ProductListBox.Items.Add(new ProductControl(item)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddProductWindow().Show();
            Close();
        }
    }
}
