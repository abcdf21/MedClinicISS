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
    /// Логика взаимодействия для Diagnosis.xaml
    /// </summary>
    public partial class Diagnosis : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Diagnosis(int selectedIndex, int id = -1)
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


        DiagnosesTableAdapter diagnoses = new DiagnosesTableAdapter();

        public void LoadDataToField()
        {
            var diagnosesData = diagnoses.GetData().Rows;
            for (int i = 0; i < diagnosesData.Count; i++)
            {
                if (Convert.ToInt32(diagnosesData[i][0]) == ID)
                {
                    Name.Text = diagnosesData[i][1].ToString();
                    Discription.Text = diagnosesData[i][2].ToString();
                }
            }
        }

        private bool IsDiagnosisNameExists(string diagnosisName)
        {
            var diagnosesData = diagnoses.GetData().Rows;
            foreach (DataRow row in diagnosesData)
            {
                if (ID != -1)
                {
                    string currentDiagnosisName = row[1].ToString();

                    if (diagnosisName.Equals(currentDiagnosisName, StringComparison.OrdinalIgnoreCase) && Convert.ToInt32(row[0]) != ID)
                    {
                        return true;
                    }
                }
                else
                {
                    if (diagnosisName.Equals(row[1].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            string diagnosisName = Name.Text.Trim();
            string diagnosisDescription = Discription.Text.Trim();

            if (string.IsNullOrEmpty(diagnosisName) || string.IsNullOrEmpty(diagnosisDescription))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            if (diagnosisName.Length > 50)
            {
                MessageBox.Show("Наименование диагноза должно содержать не более 50 символов.");
                return;
            }

            if (IsDiagnosisNameExists(diagnosisName))
            {
                MessageBox.Show("Диагноз с таким наименованием уже существует.");
                return;
            }

            if (ID != -1)
            {
                diagnoses.UpdateQuery(Name.Text, Discription.Text, ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
            else
            {
                diagnoses.InsertQuery(Name.Text, Discription.Text);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
