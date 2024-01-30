using DocumentFormat.OpenXml.Office2010.Excel;
using MedClinicISS.medCefDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Patient.xaml
    /// </summary>
    public partial class Patient : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Patient(int selectedIndex, int id = -1)
        {
            InitializeComponent();
            selectedComboBoxIndex = selectedIndex;
            ID = id;
            ChangePageToUpd();
        }

        public void ChangePageToUpd()
        {
            if (ID != -1)
            {
                add_upd.Content = "Изменить запись";

                LoadDataToField();
            }
        }

        PatientsTableAdapter patients = new PatientsTableAdapter();


        public void LoadDataToField()
        {
            var patientsData = patients.GetData().Rows;
            for (int i = 0; i < patientsData.Count; i++)
            {
                if (Convert.ToInt32(patientsData[i][0]) == ID)
                {
                    surname.Text = patientsData[i][1].ToString();
                    name.Text = patientsData[i][2].ToString();
                    patronymic.Text = patientsData[i][3].ToString();
                    dateOfBirth.Text = patientsData[i][4].ToString();
                    phoneNum.Text = patientsData[i][5].ToString();
                }
            }
        }
            private bool IsPatientExists(string surname, string name, string patronymic)
            {
                var patientsData = patients.GetData().Rows;
                foreach (DataRow row in patientsData)
                {
                    if (ID != -1)
                    {
                        string currentSurname = row[1].ToString();
                        string currentName = row[2].ToString();
                        string currentPatronymic = row[3].ToString();

                        if (surname.Equals(currentSurname, StringComparison.OrdinalIgnoreCase) &&
                            name.Equals(currentName, StringComparison.OrdinalIgnoreCase) &&
                            patronymic.Equals(currentPatronymic, StringComparison.OrdinalIgnoreCase) &&
                            Convert.ToInt32(row[0]) != ID)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (surname.Equals(row[1].ToString(), StringComparison.OrdinalIgnoreCase) &&
                            name.Equals(row[2].ToString(), StringComparison.OrdinalIgnoreCase) &&
                            patronymic.Equals(row[3].ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        
        private bool IsPhoneNumberValid(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d{11}$");
        }
        private bool IsNameValid(string name)
        {
            return name.Length <= 50;
        }

        private bool IsNameValid2(string name)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(name, @"[\d!@#$%^&*()_+{}\|:;""'<>,.?/~`]");
        }

        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(surname.Text) || string.IsNullOrEmpty(name.Text)
                || string.IsNullOrEmpty(patronymic.Text) || string.IsNullOrEmpty(phoneNum.Text) || dateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            if (!IsNameValid2(surname.Text))
            {
                MessageBox.Show("Поле Фамилия не может содержать цифры и специальные символы");
                return;
            }

            if (!IsNameValid2(name.Text))
            {
                MessageBox.Show("Поле Имя не может содержать цифры и специальные символы");
                return;
            }

            if (!IsNameValid2(patronymic.Text))
            {
                MessageBox.Show("Поле Отчество не может содержать цифры и специальные символы");
                return;
            }

            if (!IsNameValid(surname.Text) || !IsNameValid(name.Text) || !IsNameValid(patronymic.Text))
            {
                MessageBox.Show("Фамилия, имя и отчество не могут превышать 50 символов.");
                return;
            }

            if (!IsPhoneNumberValid(phoneNum.Text))
            {
                MessageBox.Show("Номер телефона должен содержать ровно 11 цифр.");
                return;
            }

            if (IsPatientExists(surname.Text, name.Text, patronymic.Text))
            {
                MessageBox.Show("Такой пациент уже есть в системе.");
                return;
            }

            if (dateOfBirth.SelectedDate > DateTime.Now)
            {
                MessageBox.Show("Дата не может превышать текущую дату.");
                return;
            }

            if (ID != -1)
            {
                patients.UpdateQuery(surname.Text, name.Text, patronymic.Text, dateOfBirth.Text, phoneNum.Text,  ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
            else
            {
                patients.InsertQuery(surname.Text, name.Text, patronymic.Text, dateOfBirth.Text, phoneNum.Text);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
