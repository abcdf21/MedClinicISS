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
using MedClinicISS.medCefDataSetTableAdapters;

namespace MedClinicISS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        AuthDataTableAdapter auths = new AuthDataTableAdapter();
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(login.Text) || string.IsNullOrWhiteSpace(password.Password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var all_logins = auths.GetData().Rows;

            bool isLoggedIn = false;

            for (int i = 0; i < all_logins.Count; i++)
            {
                if (all_logins[i][1].ToString() == login.Text &&
                    all_logins[i][2].ToString() == password.Password)
                {
                    RoleSave.roleId = (int)all_logins[i][3];
                    MainMenu page = new MainMenu(0);
                    this.Content = page;
                    isLoggedIn = true;
                    break; 
                }
            }

            if (!isLoggedIn)
            {
                MessageBox.Show("Неверный логин или пароль. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
