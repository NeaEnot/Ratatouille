using Ratatouille.Core;
using Ratatouille.FileStorage;
using System.Windows;

namespace Ratatouille.GUI
{
    public partial class App : Application
    {
        internal static IRecipeLogic RecipeLogic { get; private set; } = new RecipeLogic();
    }
}
