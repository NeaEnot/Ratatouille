using Ratatouille.GUI.Enums;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Ratatouille.GUI.Pages
{
    public partial class FieldPage : Page
    {
        internal FieldPage(FieldPageType type, string data, string name = null)
        {
            InitializeComponent();

            switch (type)
            {
                case FieldPageType.Text:
                    if (name != null)
                        stc.Children.Add(new TextBlock { Text = name, FontSize = 18 });
                    stc.Children.Add(new TextBlock { Text = data, FontSize = 16 });
                    break;
                case FieldPageType.Image:
                    stc.Children.Add(new Image { Source = new BitmapImage(new Uri(data)) });
                    break;
                case FieldPageType.ImagedText:
                    break;
            }
        }
    }
}
