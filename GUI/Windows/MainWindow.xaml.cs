using Ratatouille.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private List<Recipe> recipes;
        private Random rnd;

        public MainWindow()
        {
            InitializeComponent();

            rnd = new Random();

            List<Recipe> loadedRecipes = App.RecipeLogic.Find("");
            recipes = new List<Recipe>();
            while (loadedRecipes.Count > 0)
                recipes.Add(loadedRecipes[rnd.Next(0, loadedRecipes.Count)]);

            currentPage = 0;

            LoadData();
        }

        private void LoadData()
        {
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
            reWindow.Closing += (object sender, CancelEventArgs e) => LoadData();
            reWindow.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchString = tbSearch.Text;
            List<Recipe> loadedRecipes = App.RecipeLogic.Find(searchString);

            if (searchString == "")
            {
                recipes = new List<Recipe>();
                while (loadedRecipes.Count > 0)
                    recipes.Add(loadedRecipes[rnd.Next(0, loadedRecipes.Count)]);
            }
            else
            {
                recipes = loadedRecipes;
            }

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
                    LoadData();
                    break;
            }
        }
    }
}
