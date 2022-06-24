using System;
using System.Windows;
using System.Windows.Controls;

namespace Ratatouille.GUI.Pages
{
    public partial class ImageItemPage : Page
    {
        public Action<string> Delete { get; set; }

        public string Value
        { 
            get { return tb.Text; }
            set { tb.Text = value; }
        }

        public ImageItemPage()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Delete != null)
                Delete(Name);
        }
    }
}
