using Ratatouille.Core;
using Ratatouille.GUI.Pages;
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

            frameIngridients.Content = new ImagedFieldPage(recipe.Ingredients, recipe.Images);
            frameTools.Content = new ImagedFieldPage(recipe.Tools, recipe.Images);
            frameInstruction.Content = new ImagedFieldPage(recipe.Instruction, recipe.Images);
            frameNotes.Content = new ImagedFieldPage(recipe.Notes, recipe.Images);
        }

        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            string destinationurl = (sender as Hyperlink).NavigateUri.ToString();
            ProcessStartInfo sInfo = new ProcessStartInfo(destinationurl) { UseShellExecute = true, };
            Process.Start(sInfo);
        }
    }
}
