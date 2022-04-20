using Ratatouille.Core;
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

namespace Ratatouille.GUI.Windows
{
    public partial class MainWindow : Window
    {
        private int currentPage;
        private int countRecipes = 20;

        private string searchString = "";

        public MainWindow()
        {
            InitializeComponent();
            currentPage = 0;
            LoadData();
        }

        private void LoadData()
        {
            List<Recipe> recipes = App.RecipeLogic.Find(searchString);

            lbRecipes.ItemsSource = null;
            lbRecipes.ItemsSource = recipes.Skip(currentPage * countRecipes).Take(countRecipes);

            if (recipes.Count - countRecipes * currentPage > countRecipes)
                btnNext.IsEnabled = true;
            else
                btnNext.IsEnabled = false;
            if (currentPage > 0)
                btnPrev.IsEnabled = true;
            else
                btnPrev.IsEnabled = false;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            currentPage--;
            LoadData();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            currentPage++;
            LoadData();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lblView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblUpdate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
