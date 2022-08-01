using Ratatouille.Core;
using Ratatouille.GUI.Pages;
using System.Collections.Generic;
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

            Dictionary<string, Field> fields =
                new Dictionary<string, Field>
                {
                    { recipe.Tags, new Field { Title = tblTagsTitle, Content = tblTags } },
                    { recipe.ApproxTime, new Field { Title = tblApproxTimeTitle, Content = tblApproxTime } },
                    { recipe.Ingredients, new Field { Title = tblIngridientsTitile, Content = frameIngridients } },
                    { recipe.Tools, new Field { Title = tblToolsTitile, Content = frameTools } },
                    { recipe.Instruction, new Field { Title = tblInstructionTitile, Content = frameInstruction } },
                    { recipe.Notes, new Field { Title = tblNotesTitile, Content = frameNotes } },
                    { recipe.Links.Count > 0 ? recipe.Links[0] : "", new Field { Title = tblLinksTitle, Content = lbLinks } }
                };

            foreach (string key in fields.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    fields[key].Title.Visibility = Visibility.Hidden;
                    fields[key].Content.Visibility = Visibility.Hidden;
                }
            }
        }

        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            string destinationurl = (sender as Hyperlink).NavigateUri.ToString();
            ProcessStartInfo sInfo = new ProcessStartInfo(destinationurl) { UseShellExecute = true, };
            Process.Start(sInfo);
        }

        private class Field
        {
            internal UIElement Title { get; set; }
            internal UIElement Content { get; set; }
        }
    }
}
