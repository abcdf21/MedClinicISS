﻿using ClosedXML.Excel;
using MaterialDesignColors;
using MedClinicISS.medCefDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private int RoleId;
        private List<Button> buttonList;

        public MainMenu(int selectedIndex)
        {
            InitializeComponent();
            buttonListInit();
            RoleId = RoleSave.roleId;
            roleNameChange();
            tablesComboBox.SelectedIndex = selectedIndex;
        }

        PatientsTableAdapter patients = new PatientsTableAdapter();
        DiagnosesTableAdapter diagnoses = new DiagnosesTableAdapter();
        AppointmentsTableAdapter appointments = new AppointmentsTableAdapter();
        RecipesTableAdapter recipes = new RecipesTableAdapter();
        ResultsTableAdapter results = new ResultsTableAdapter();
        VisitsTableAdapter visits = new VisitsTableAdapter();
        RolesTableAdapter roles = new RolesTableAdapter();
        AuthDataTableAdapter auths = new AuthDataTableAdapter();

        public void roleNameChange()
        {
            if (RoleId == 1)
            {
                RoleName.Text = "Администратор";
            }
            else
            {
                RoleName.Text = "Врач";
                tablesComboBox.Items.RemoveAt(7);
                tablesComboBox.Items.RemoveAt(6);
            }
        }

        public void TableDataLoad()
        {
            switch (tablesComboBox.SelectedIndex)
            {
                case 0:
                    table.ItemsSource = patients.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 1:
                    table.ItemsSource = diagnoses.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 2:
                    table.ItemsSource = appointments.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 3:
                    table.ItemsSource = recipes.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 4:
                    table.ItemsSource = results.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 5:
                    table.ItemsSource = visits.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 6:
                    table.ItemsSource = roles.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                case 7:
                    table.ItemsSource = auths.GetData();
                    table.AutoGeneratedColumns += Table_AutoGeneratedColumns;
                    break;
                default:
                    break;
            }
        }

        private void Table_AutoGeneratedColumns(object sender, EventArgs e)
        {
            switch (tablesComboBox.SelectedIndex)
            {
                case 0:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Фамилия пациента";
                    table.Columns[2].Header = "Имя пациента";
                    table.Columns[3].Header = "Отчество пациента";
                    table.Columns[4].Header = "День рождения";
                    if (table.Columns[4] is DataGridTextColumn dateColumn1)
                        dateColumn1.Binding.StringFormat = "dd.MM.yyyy";
                    table.Columns[5].Header = "Номер телефона";
                    break;
                case 1:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Название диагноза";
                    table.Columns[2].Header = "Описание диагноза";
                    break;
                case 2:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Название назначения";
                    table.Columns[2].Header = "Описание назначения";
                    break;
                case 3:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Название рецепта";
                    table.Columns[2].Header = "Описание рецепта";
                    break;
                case 4:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Результат анализа";
                    break;
                case 5:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Пациент";
                    table.Columns[2].Header = "Дата визита";
                    if (table.Columns[2] is DataGridTextColumn dateColumn2)
                        dateColumn2.Binding.StringFormat = "dd.MM.yyyy";
                    table.Columns[3].Header = "Диагноз";
                    table.Columns[4].Header = "Результат анализа";
                    table.Columns[5].Header = "Назначение";
                    table.Columns[6].Header = "Рецепт";
                    break;
                case 6:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Роль";
                    break;
                case 7:
                    table.Columns[0].Header = "ID";
                    table.Columns[1].Header = "Логин";
                    table.Columns[2].Header = "Пароль";
                    table.Columns[3].Header = "Роль";
                    break;
                default:
                    break;
            }
        }

        public void buttonListInit()
        {
            buttonList = new List<Button>();
            buttonList.Add(add);
            buttonList.Add(upd);
            buttonList.Add(del);
        }

        public void updateTableNameText()
        {
            ComboBoxItem selectedItem = (ComboBoxItem)tablesComboBox.SelectedItem;
            TableName.Text = selectedItem.Content.ToString();
        }

        public void DisableButton()
        {
            if (tablesComboBox.SelectedIndex == 6)
            {
                foreach (Button button in buttonList)
                {
                    button.IsEnabled = false;
                    button.Opacity = 0.8;
                }
            }
            else if (tablesComboBox.SelectedIndex == 0)
            {
                export.Visibility = Visibility.Visible;
                searchField.Visibility = Visibility.Visible;
                search.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (Button button in buttonList)
                {
                    button.IsEnabled = true;
                    button.Opacity = 1;
                }

                export.Visibility = Visibility.Collapsed;
                searchField.Visibility = Visibility.Collapsed;
                search.Visibility = Visibility.Collapsed;
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = tablesComboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    mainMenuFrame.Content = new Patient(selectedIndex);
                    break;
                case 1:
                    mainMenuFrame.Content = new Diagnosis(selectedIndex);
                    break;
                case 2:
                    mainMenuFrame.Content = new Appointments(selectedIndex);
                    break;
                case 3:
                    mainMenuFrame.Content = new Recipes(selectedIndex);
                    break;
                case 4:
                    mainMenuFrame.Content = new Result(selectedIndex);
                    break;
                case 5:
                    mainMenuFrame.Content = new Visits(selectedIndex);
                    break;
                case 7:
                    mainMenuFrame.Content = new Registration(selectedIndex);
                    break;
                default:
                    break;
            }
        }

        private void upd_Click(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem != null)
            {
                int id = Convert.ToInt32((table.SelectedItem as DataRowView).Row[0]);
                int selectedIndex = tablesComboBox.SelectedIndex;
                switch (selectedIndex)
                {
                    case 0:
                        mainMenuFrame.Content = new Patient(selectedIndex, id);
                        break;
                    case 1:
                        mainMenuFrame.Content = new Diagnosis(selectedIndex, id);
                        break;
                    case 2:
                        mainMenuFrame.Content = new Appointments(selectedIndex, id);
                        break;
                    case 3:
                        mainMenuFrame.Content = new Recipes(selectedIndex, id);
                        break;
                    case 4:
                        mainMenuFrame.Content = new Result(selectedIndex, id);
                        break;
                    case 5:
                        mainMenuFrame.Content = new Visits(selectedIndex, id);
                        break;
                    case 7:
                        mainMenuFrame.Content = new Registration(selectedIndex, id);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите запись в таблице для изменения.");
            }
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            if (table.SelectedItem != null)
            {
                int id = Convert.ToInt32((table.SelectedItem as DataRowView).Row[0]);
                int selectedIndex = tablesComboBox.SelectedIndex;
                switch (selectedIndex)
                {
                    case 0:
                        patients.DeleteQuery(id);
                        break;
                    case 1:
                        diagnoses.DeleteQuery(id);
                        break;
                    case 2:
                        appointments.DeleteQuery(id);
                        break;
                    case 3:
                        recipes.DeleteQuery(id);
                        break;
                    case 4:
                        results.DeleteQuery(id);
                        break;
                    case 5:
                        visits.DeleteQuery(id);
                        break;
                    case 7:
                        auths.DeleteQuery(id);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Выберите запись в таблице для удаления.");
            }
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable patientDataTable = patients.GetData();

                if (patientDataTable.Rows.Count > 0)
                {
                    var wb = new XLWorkbook();
                    var ws = wb.Worksheets.Add("Пациенты");
                    ws.Cell(1, 1).InsertTable(patientDataTable);

                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                    saveFileDialog.Filter = "Excel файл (*.xlsx)|*.xlsx";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Данные пациентов успешно экспортированы в Excel.");
                    }
                }
                else
                {
                    MessageBox.Show("Таблица пациентов пуста.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при экспорте данных пациентов: " + ex.Message);
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchField.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                TableDataLoad();
                return;
            }

            List<PatientClass> filtered = new List<PatientClass>();

            foreach (MedClinicISS.medCefDataSet.PatientsRow patientRow in patients.GetData().Rows)
            {
                PatientClass patient = new PatientClass(
                    patientRow.patient_id,
                    patientRow.patient_surname,
                    patientRow.patient_name,
                    patientRow.patient_patronymic,
                    patientRow.patient_dateOfBirth,
                    patientRow.patient_phoneNumber
                );

                if (patient.Surname.Contains(searchText))
                {
                    filtered.Add(patient);
                }
            }

            table.ItemsSource = filtered;
        }

        private void tablesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateTableNameText();
            TableDataLoad();
            DisableButton();
        }

    }
}