using Ratatouille.Core;
using System.Windows;
using System.Windows.Navigation;

namespace Ratatouille.GUI.Windows
{
    public partial class RecipeViewWindow : Window
    {
        public RecipeViewWindow(Recipe recipe)
        {
            InitializeComponent();
            DataContext = recipe;
        }

        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {

        }
    }
}
