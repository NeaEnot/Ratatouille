using Microsoft.Win32;
using Ratatouille.Core;
using Ratatouille.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Ratatouille.GUI.Windows
{
    public partial class RecipeEditWindow : Window
    {
        private RecipeViewModel model;

        private static Random rnd = new Random();

        public RecipeEditWindow(Recipe recipe)
        {
            InitializeComponent();

            model = new RecipeViewModel(recipe);

            DataContext = model;

            foreach (string img in recipe.Images)
                tbImages.Text += img + '\n';

            foreach (string link in recipe.Links)
                tbLinks.Text += link + '\n';
        }

        private void btnSelectThumb_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Изображение (*.jpg;*.jpeg) | *.jpg;*.jpeg";

            if (dlg.ShowDialog() == true)
                model.Thumbnail = dlg.FileName;
        }

        private void tbImages_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] imgs = tbImages.Text.Split('\n');
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> imgs = new List<string>();
            List<string> links = new List<string>();

            foreach (string img in tbImages.Text.Split('\n').Where(req => req != ""))
                imgs.Add(img);

            foreach (string link in tbLinks.Text.Split('\n').Where(req => req != ""))
                links.Add(link);

            model.Images = imgs;
            model.Links = links;

            if (model.Id == null)
                App.RecipeLogic.Create(model.Recipe);
            else
                App.RecipeLogic.Update(model.Recipe);
        }
    }
}
