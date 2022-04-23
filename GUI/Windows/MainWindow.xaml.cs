using Ratatouille.Core;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            //List<Recipe> recipes = App.RecipeLogic.Find(searchString);
            List<Recipe> recipes = new List<Recipe>()
            {
                new Recipe
                { 
                    Name = "Салат \"Охотничий\" с копченой колбасой",
                    ApproxTime = "60 минут",
                    Thumbnail = "https://img1.russianfood.com/dycontent/images_upl/170/big_169503.jpg",
                    Tags = "Выпечка, Закуска",
                    Ingredients = "Колбаса сырокопченая - 200 г\nСыр твердый - 200 г\nКартофель отварной - 3 шт.\nОгурцы маринованные - 3-4 шт.\nГорошек зеленый консервированный - 1 банка\nЛук фиолетовый небольшой - 1 шт.\nСоль - по вкусу\nПерец черный молотый - по вкусу\nМайонез - 2 ст.л. (по вкусу)",
                    Tools = "Нож, разделочная доска, тарелка",
                    Instruction = "Подготавливаем продукты для салата с колбасой, сыром, огурцами, горошком и картофелем.\n\nКартофель заранее отвариваем, остужаем, очищаем.\n\nКолбасу нарезаем соломкой.\n\nОгурчики нарезаем соломкой.\n\nОтварной картофель тоже нарезаем соломкой.\n\nЛук нарезаем полукольцами или соломкой.\n\nТвердый сыр трем на терке.\n\nСмешиваем все нарезанные ингредиенты. Добавляем в салат с сыром, колбасой, картофелем и огурцами консервированный горошек, молотый черный перец и майонез. Все перемешиваем.\n\nСалат \"Охотничий\" с копченой колбасой готов. Приятного аппетита!",
                }
            };

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
            searchString = tbSearch.Text;
            currentPage = 0;
            LoadData();
        }

        private void lblView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RecipeViewWindow window = new RecipeViewWindow((sender as Label).DataContext as Recipe);
            window.Show();
        }

        private void lblUpdate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void lblDelete_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
