using Ratatouille.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

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
            RecipeEditWindow reWindow = new RecipeEditWindow(new Recipe());
            reWindow.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            searchString = tbSearch.Text;
            currentPage = 0;
            LoadData();
        }

        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            switch ((sender as Hyperlink).NavigateUri.ToString())
            {
                case "/View":
                    RecipeViewWindow rvWindow = new RecipeViewWindow((sender as Hyperlink).DataContext as Recipe);
                    rvWindow.Show();
                    break;
                case "/Update":
                    RecipeEditWindow reWindow = new RecipeEditWindow((sender as Hyperlink).DataContext as Recipe);
                    reWindow.Show();
                    break;
                case "/Delete":
                    App.RecipeLogic.Delete(((sender as Hyperlink).DataContext as Recipe).Id);
                    break;
            }
        }
    }
}
