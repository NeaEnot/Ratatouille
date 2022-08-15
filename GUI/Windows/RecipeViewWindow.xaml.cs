using Ratatouille.Core;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
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
            string destinationurl = (sender as Hyperlink).NavigateUri.ToString();
            ProcessStartInfo sInfo = new ProcessStartInfo(destinationurl) { UseShellExecute = true };
            Process.Start(sInfo);
        }
    }
}
