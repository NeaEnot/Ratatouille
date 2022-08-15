using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ratatouille.GUI.Pages
{
    public partial class ImagedFieldPage : Page
    {
        private static Regex regex = new Regex(@"(<img_\d+;\d+x\d+>)");
        private static Regex digit = new Regex(@"\d+");

        public ImagedFieldPage(string text, List<string> imgs)
        {
            InitializeComponent();

            if (text != null)
            {
                string[] elements = regex.Split(text);
                
                foreach (string element in elements)
                {
                    if (string.IsNullOrWhiteSpace(element))
                        continue;
                    
                    if (regex.IsMatch(element))
                    {
                        List<int> digits = digit.Matches(element).Select(req => int.Parse(req.Value)).ToList();

                        Image image = new Image
                        {
                            Stretch = Stretch.Uniform,
                            Source = new BitmapImage(new Uri(imgs[digits[0]]))
                        };

                        if (digits[1] != 0)
                            image.Width = digits[1];
                        if (digits[2] != 0)
                            image.Height = digits[2];

                        panel.Children.Add(image);
                    }
                    else
                    {
                        TextBlock tb = new TextBlock
                        {
                            Text = element,
                            FontSize = 16,
                            TextWrapping = TextWrapping.Wrap
                        };

                        panel.Children.Add(tb);
                    }
                }
            }
        }
    }
}
