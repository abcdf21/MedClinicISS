using MedClinicISS.medCefDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

namespace MedClinicISS
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Registration(int selectedIndex, int id = -1)
        {
            InitializeComponent();
            selectedComboBoxIndex = selectedIndex;
            ID = id;
            LoadRolesIntoComboBox();
            ChangePageToUpd();
        }

        RolesTableAdapter roles = new RolesTableAdapter();
        AuthDataTableAdapter auths = new AuthDataTableAdapter();

        public void LoadDataToField()
        {
            var authsData = auths.GetData().Rows;
            for (int i = 0; i < authsData.Count; i++)
            {
                if (Convert.ToInt32(authsData[i][0]) == ID)
                {
                    Login.Text = authsData[i][1].ToString();
                    Password.Text = authsData[i][2].ToString();
                    Role.SelectedValue = Convert.ToInt32(authsData[i][3]);
                }
            }
        }

        public void ChangePageToUpd()
        {
            if (ID != -1)
            {
                add_upd.Content = "Изменить запись";

                LoadDataToField();
            }
        }
        public void LoadRolesIntoComboBox()
        {
            var rolesData = roles.GetData();

            Role.ItemsSource = rolesData;

            Role.DisplayMemberPath = "role_name";
            Role.SelectedValuePath = "role_id";
        }

        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Login.Text.Length < 5 || Login.Text.Length > 20)
            {
                MessageBox.Show("Логин должен содержать от 5 до 20 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsLoginValid(Login.Text))
            {
                MessageBox.Show("Логин не должен содержать специальные символы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsLoginExists(Login.Text))
            {
                MessageBox.Show("Логин уже существует в системе. Выберите другой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Password.Text.Length < 6 || Password.Text.Length > 18)
            {
                MessageBox.Show("Пароль должен содержать от 6 до 18 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsPasswordValid(Password.Text))
            {
                MessageBox.Show("Пароль должен содержать как минимум 2 цифры и 1 специальный символ.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Role.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ID != -1)
            {
                auths.UpdateQuery(Login.Text, Password.Text, (int)Role.SelectedValue, ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }

            else
            {
                auths.InsertQuery(Login.Text, Password.Text, (int)Role.SelectedValue);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }

        }

        private bool IsLoginValid(string login)
        {
            foreach (char c in login)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsPasswordValid(string password)
        {
            int digitCount = 0;
            int specialCharCount = 0;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    digitCount++;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    specialCharCount++;
                }
            }

            return digitCount >= 2 && specialCharCount >= 1;
        }

        private bool IsLoginExists(string login)
        {
            var authsData = auths.GetData().Rows;

            foreach (DataRow row in authsData)
            {
                string currentLogin = row[1].ToString();

                if (login.Equals(currentLogin, StringComparison.OrdinalIgnoreCase) && Convert.ToInt32(row[0]) != ID)
                {
                    return true;
                }
            }

            return false;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
