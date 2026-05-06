using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var DB = new PaladinDe42Context();
            var users = DB.Users;
            var login = UserLoginBox.Text;
            var password = UserPasswordBox.Password;
            var User = users.Where(x => x.Login == login
            && x.Password == password).FirstOrDefault();

            User = new PaladinDe42Context()
                 .Users.Where(x => x.Login == UserLoginBox.Text
                 && x.Password == UserPasswordBox.Password)
                 .FirstOrDefault();

            
            if(User != null)
            {
                MessageBox.Show("Вы успешно авторизировались");
                UserSingleton.GetUser = User;
                new ListProductWindow().Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин и пароль!", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}