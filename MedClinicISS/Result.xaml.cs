using DocumentFormat.OpenXml.Office2010.Excel;
using MedClinicISS;
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
    /// Логика взаимодействия для Result.xaml
    /// </summary>
    public partial class Result : Page
    {

        private int selectedComboBoxIndex;
        private int ID;
        public Result(int selectedIndex, int id = -1)
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

        ResultsTableAdapter results = new ResultsTableAdapter();


        public void LoadDataToField()
        {
            var resultssData = results.GetData().Rows;
            for (int i = 0; i < resultssData.Count; i++)
            {
                if (Convert.ToInt32(resultssData[i][0]) == ID)
                {
                    Name.Text = resultssData[i][1].ToString();
                }
            }
        }

        private void add_upd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                MessageBox.Show("Введите результаты анализа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsResultExists(Name.Text))
            {
                MessageBox.Show("Такие результаты анализа уже существуют в системе.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ID != -1)
            {
                results.UpdateQuery(Name.Text, ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
            else
            {
                results.InsertQuery(Name.Text);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }

        private bool IsResultExists(string result)
        {
            var resultsData = results.GetData().Rows;
            foreach (DataRow row in resultsData)
            {
                if (ID != -1)
                {
                    string currentResult = row[1].ToString();

                    if (result.Equals(currentResult, StringComparison.OrdinalIgnoreCase) && Convert.ToInt32(row[0]) != ID)
                    {
                        return true;
                    }
                }
                else
                {
                    if (result.Equals(row[1].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
