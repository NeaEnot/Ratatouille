using System.Windows;

namespace Ratatouille.GUI.Windows
{
    public partial class EnterWindow : Window
    {
        public string Text { get; private set; }

        public EnterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text = tbText.Text;
            DialogResult = true;
        }
    }
}
