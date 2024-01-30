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
    /// Логика взаимодействия для Recipes.xaml
    /// </summary>
    public partial class Recipes : Page
    {
        private int selectedComboBoxIndex;
        private int ID;
        public Recipes(int selectedIndex, int id = -1)
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


        RecipesTableAdapter recipes = new RecipesTableAdapter();

        public void LoadDataToField()
        {
            var recipesData = recipes.GetData().Rows;
            for (int i = 0; i < recipesData.Count; i++)
            {
                if (Convert.ToInt32(recipesData[i][0]) == ID)
                {
                    Name.Text = recipesData[i][1].ToString();
                    Discription.Text = recipesData[i][2].ToString();
                }
            }
        }

        private bool IsDrugNameExists(string drugName)
        {
            var recipesData = recipes.GetData().Rows;
            foreach (DataRow row in recipesData)
            {
                if (ID != -1)
                {
                    string currentRecipeName = row[1].ToString();

                    if (drugName.Equals(currentRecipeName, StringComparison.OrdinalIgnoreCase) && Convert.ToInt32(row[0]) != ID)
                    {
                        return true;
                    }
                }
                else
                {
                    if (drugName.Equals(row[1].ToString(), StringComparison.OrdinalIgnoreCase))
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
                MessageBox.Show("Наименование препарата должно содержать не более 50 символов.");
                return;
            }

            if (IsDrugNameExists(Name.Text))
            {
                MessageBox.Show("Препарат с таким наименованием уже существует.");
                return;
            }

            if (ID != -1)
            {
                recipes.UpdateQuery(Name.Text, Discription.Text, ID);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
            else
            {
                recipes.InsertQuery(Name.Text, Discription.Text);
                backFrame.Content = new MainMenu(selectedComboBoxIndex);
            }
        }
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            backFrame.Content = new MainMenu(selectedComboBoxIndex);
        }
    }
}
