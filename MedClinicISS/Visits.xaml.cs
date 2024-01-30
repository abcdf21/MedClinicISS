using DocumentFormat.OpenXml.Wordprocessing;
using MedClinicISS.medCefDataSetTableAdapters;
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

namespace MedClinicISS
{
    /// <summary>
    /// Логика взаимодействия для Visits.xaml
    /// </summary>
    public partial class Visits : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Visits(int selectedIndex, int id = -1)
        {
            InitializeComponent();
            selectedComboBoxIndex = selectedIndex;
            ID = id;
            LoadDataIntoComboBox();
            ChangePageToUpd();
        }

        public void ChangePageToUpd()
        {
            if (ID != -1)
            {
                add_upd.Content = "Изменить запись";
                LoadDataToField();
            }
            else
            {
                visitDate.SelectedDate = DateTime.Now;
            }
        }

        PatientsTableAdapter patients = new PatientsTableAdapter();
        DiagnosesTableAdapter diagnoses = new DiagnosesTableAdapter();
        AppointmentsTableAdapter appointments = new AppointmentsTableAdapter();
        RecipesTableAdapter recipes = new RecipesTableAdapter();
        ResultsTableAdapter results = new ResultsTableAdapter();
        VisitsTableAdapter visits = new VisitsTableAdapter();


        public void LoadDataToField()
        {
            var visitssData = visits.GetData().Rows;
            for (int i = 0; i < visitssData.Count; i++)
            {
                if (Convert.ToInt32(visitssData[i][0]) == ID)
                {
                    PatinetComboBox.SelectedValue = visitssData[i][1];
                    visitDate.Text = visitssData[i][2].ToString();
                    DiagnosComboBox.SelectedValue = visitssData[i][3];
                    ResultComboBox.SelectedValue = visitssData[i][4];
                    AppointmentComboBox.SelectedValue = visitssData[i][5];
                    RecipeComboBox.SelectedValue = visitssData[i][6];

                }
            }
        }


        public void LoadDataIntoComboBox()
        {
            
            
            PatinetComboBox.ItemsSource = patients.GetData();
            DiagnosComboBox.ItemsSource = diagnoses.GetData(); ;
            ResultComboBox.ItemsSource = results.GetData(); ;
            AppointmentComboBox.ItemsSource = appointments.GetData(); ;
            RecipeComboBox.ItemsSource = recipes.GetData();

            PatinetComboBox.DisplayMemberPath = "patient_surname"; 
            PatinetComboBox.SelectedValuePath = "patient_id"; 

            DiagnosComboBox.DisplayMemberPath = "diagnosis_name"; 
            DiagnosComboBox.SelectedValuePath = "diagnosis_id";

            ResultComboBox.DisplayMemberPath = "results"; 
            ResultComboBox.SelectedValuePath = "result_id"; 

            AppointmentComboBox.DisplayMemberPath = "appointments_name";
            AppointmentComboBox.SelectedValuePath = "appointments_id";

            RecipeComboBox.DisplayMemberPath = "drug_name";
            RecipeComboBox.SelectedValuePath = "recipes_id";
        }



        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            if (PatinetComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите пациента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DiagnosComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите диагноз", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (AppointmentComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите назначение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (RecipeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите рецепт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ResultComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите результат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (visitDate.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату визита", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
