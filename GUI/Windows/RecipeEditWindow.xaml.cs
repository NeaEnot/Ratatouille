using Microsoft.Win32;
using Ratatouille.Core;
using Ratatouille.GUI.Pages;
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

        private Dictionary<string, ImageItemPage> imgPages = new Dictionary<string, ImageItemPage>();
        private Dictionary<string, Frame> imgFrames = new Dictionary<string, Frame>();

        private static Random rnd = new Random();

        public RecipeEditWindow(Recipe recipe)
        {
            InitializeComponent();

            model = new RecipeViewModel(recipe);

            DataContext = model;

            foreach (string img in recipe.Images)
                spImages.Children.Add(CreateImageFrame(img));

            foreach (string link in recipe.Links)
                tbLinks.Text += link + "\n";
        }

        private void btnSelectThumb_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Изображение (*.jpg;*.jpeg) | *.jpg;*.jpeg";

            if (dlg.ShowDialog() == true)
                model.Thumbnail = dlg.FileName;
        }

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            spImages.Children.Add(CreateImageFrame(""));
        }

        private void DeleteImg(string name)
        {
            spImages.Children.Remove(imgFrames[name]);
            imgPages.Remove(name);
            imgFrames.Remove(name);
        }

        private Frame CreateImageFrame(string link)
        {
            string name = "img_" + rnd.Next().ToString();
            ImageItemPage page = new ImageItemPage { Name = name, Value = link, Delete = DeleteImg };
            Frame frame = new Frame { Name = name, Content = page };

            imgPages.Add(name, page);
            imgFrames.Add(name, frame);

            return frame;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> imgs = new List<string>();
            List<string> links = new List<string>();

            foreach (string key in imgPages.Keys)
                imgs.Add(imgPages[key].Value);

            foreach (string link in tbLinks.Text.Split('\n').Where(req => req != ""))
                links.Add(link);

            model.Images = imgs;
            model.Links = links;

            if (model.Id != null)
                App.RecipeLogic.Create(model.Recipe);
            else
                App.RecipeLogic.Update(model.Recipe);
        }
    }
}
