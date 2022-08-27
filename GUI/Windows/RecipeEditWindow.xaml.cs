using Microsoft.Win32;
using Ratatouille.Core;
using Ratatouille.GUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Ratatouille.GUI.Windows
{
    public partial class RecipeEditWindow : Window
    {
        private RecipeViewModel model;
        
        private Dictionary<string, string> imgLinks;
        private Stack<Label> labels;

        private static Random rnd = new Random();

        public RecipeEditWindow(Recipe recipe)
        {
            InitializeComponent();

            model = new RecipeViewModel(recipe);

            DataContext = model;

            imgLinks = new Dictionary<string, string>();
            labels = new Stack<Label>();

            foreach (string img in recipe.Images)
                tbImages.Text += img + '\n';

            foreach (string link in recipe.Links)
            {
                string guid = Guid.NewGuid().ToString();
                imgLinks.Add(guid, link);
                tbLinks.Text += guid + '\n';
            }
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

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                int n = 0;
                for (int i = tbImages.Text.Split('\n').Length; i > 0; i /= 10)
                    n++;

                numbersColumn.Width = new GridLength(n * 15);
                wpImages.Children.Clear();

                while (labels.Count < tbImages.Text.Split('\n').Length)
                {
                    Label label = new Label
                    {
                        Content = labels.Count,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Padding = new Thickness(0, 0.0025, 4, 0),
                        FontSize = 16
                    };

                    labels.Push(label);
                    spNumbers.Children.Add(label);
                }

                while (labels.Count > tbImages.Text.Split('\n').Length)
                {
                    Label label = labels.Pop();
                    spNumbers.Children.Remove(label);
                }

                int number = -1;
                foreach (string link in tbImages.Text.Split('\n'))
                {
                    number++;

                    if (string.IsNullOrWhiteSpace(link))
                        continue;

                    StackPanel sp = new StackPanel { Orientation = Orientation.Horizontal };

                    Label label = new Label
                    {
                        Content = number + ":",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Padding = new Thickness(0, 0.0025, 4, 0),
                        FontSize = 16
                    };

                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(imgLinks.ContainsKey(link) ? imgLinks[link] : link);
                    bmp.EndInit();

                    Image image = new Image
                    {
                        Source = bmp,
                        Width = 150,
                        Height = 150,
                        Stretch = Stretch.Uniform
                    };

                    sp.Children.Add(label);
                    sp.Children.Add(image);
                    wpImages.Children.Add(sp);

                    AddFromAvailable.Items.Add(new MenuItem { Header = $"{number}: {link}", Command = null, CommandParameter = number });
                }
            }));
        }

        //public ICommand MenuItemDelete =>
        //    new Command<int>((purposeModel) =>
        //    {

        //    });


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> imgs = new List<string>();
            List<string> links = new List<string>();

            foreach (string img in tbImages.Text.Split('\n').Where(req => req != ""))
                imgs.Add(img);

            foreach (string link in tbLinks.Text.Split('\n').Where(req => req != ""))
            {
                if (imgLinks.ContainsKey(link))
                    links.Add(imgLinks[link]);
                else
                    links.Add(link);
            }

            model.Images = imgs;
            model.Links = links;

            if (model.Id == null)
                App.RecipeLogic.Create(model.Recipe);
            else
                App.RecipeLogic.Update(model.Recipe);
        }
    }
}
