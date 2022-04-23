using Ratatouille.Core;
using Ratatouille.GUI.Enums;
using Ratatouille.GUI.Pages;
using System.Windows;

namespace Ratatouille.GUI.Windows
{
    public partial class RecipeViewWindow : Window
    {
        public RecipeViewWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = recipe;
        }
    }
}
