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
using System.Windows.Shapes;

namespace Ratatouille.GUI.Windows
{
    public partial class RecipeEditWindow : Window
    {
        private Recipe recipe;

        public RecipeEditWindow(Recipe recipe)
        {
            InitializeComponent();

            this.recipe = recipe;

            DataContext = recipe;
        }

        private void btnSelectThumb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddImg_Click(object sender, RoutedEventArgs e)
        {
            List<string> links = new List<string>();
            foreach (string item in lbImages.Items)
                links.Add(item);

            links.Add("");

            lbImages.ItemsSource = null;
            lbImages.ItemsSource = links;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
