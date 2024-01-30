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
    /// Логика взаимодействия для Appointments.xaml
    /// </summary>
    public partial class Appointments : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Appointments(int selectedIndex, int id = -1)
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


        AppointmentsTableAdapter appointments = new AppointmentsTableAdapter();

        public void LoadDataToField()
        {
            var apointData = appointments.GetData().Rows;
            for (int i = 0; i < apointData.Count; i++)
            {
                if (Convert.ToInt32(apointData[i][0]) == ID)
                {
                    Name.Text = apointData[i][1].ToString();
                    Discription.Text = apointData[i][2].ToString();
                }
            }
        }

        private bool IsAppointmentNameExists(string appointmentName)
        {
            var appointmentsData = appointments.GetData().Rows;
            foreach (DataRow row in appointmentsData)
            {
                if (ID != -1)
                {
                    string currentAppointmentName = row[1].ToString();

                    if (appointmentName.Equals(currentAppointmentName, StringComparison.OrdinalIgnoreCase) && Convert.ToInt32(row[0]) != ID)
                    {
                        return true;
                    }
                }
                else
                {
                    if (appointmentName.Equals(row[1].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text) || string.IsNullOrEmpty(Discription.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (Name.Text.Length > 50)
            {
                MessageBox.Show("Наименование назначения должно содержать не более 50 символов.");
                return;
            }

            if (IsAppointmentNameExists(Name.Text))
            {
                MessageBox.Show("Назначение с таким наименованием уже существует.");
                return;
            }

            if (ID != -1)
            {
                appointments.UpdateQuery(Name.Text, Discription.Text, ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
            else
            {
                appointments.InsertQuery(Name.Text, Discription.Text);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
