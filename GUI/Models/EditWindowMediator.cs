using Ratatouille.GUI.Controls;
using Ratatouille.GUI.Windows;
using System.Collections.Generic;

namespace Ratatouille.GUI.Models
{
    internal class EditWindowMediator
    {
        private List<ContextedTextBox> contextedTextBoxes;
        private RecipeEditWindow recipeEditWindow;

        internal EditWindowMediator(RecipeEditWindow recipeEditWindow)
        {
            this.recipeEditWindow = recipeEditWindow;
            contextedTextBoxes = new List<ContextedTextBox>();
        }

        internal void RegisterContextedTextBox(ContextedTextBox contextedTextBox)
        {
            if (!contextedTextBoxes.Contains(contextedTextBox))
                contextedTextBoxes.Add(contextedTextBox);
        }

        internal void UpdateLinxsInContexts(List<string> links)
        {
            foreach (ContextedTextBox contextedTextBox in contextedTextBoxes)
                contextedTextBox.UpdateCurrentLinks(links);
        }
    }
}
