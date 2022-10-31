using Ratatouille.GUI.Windows;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ratatouille.GUI.Controls
{
    public partial class ContextedTextBox : UserControl
    {
        public string Text
        {
            get => tb.Text;
            set { tb.Text = value; SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty;

        public Action<List<string>> UpdateLinks;

        static ContextedTextBox()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ContextedTextBox));
        }

        public ContextedTextBox()
        {
            InitializeComponent();
            UpdateLinks = UpdateCurrentLinks;
        }

        private void UpdateCurrentLinks(List<string> links)
        {
            miInsertFromCurrent.Items.Clear();

            for (int i = 0; i < links.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(links[i]))
                    continue;

                string s = $"<img_{i};400x300>";

                MenuItem item = new MenuItem
                {
                    Header = $"{i}: {links[i]}",
                    Command =
                        new Command(e => 
                        {
                            tb.Text = tb.Text.Insert(tb.CaretIndex, s);
                        })
                };

                miInsertFromCurrent.Items.Add(item);
            }
        }

        private void miInsertFromInternet_Click(object sender, RoutedEventArgs e)
        {
            EnterWindow window = new EnterWindow();
            if (window.ShowDialog() == true)
            {
                // Добавить изображение к списку изображений
                // Вставить тег с учётом номера изображения
            }
        }

        private class Command : ICommand
        {
            private Action<object> execute;
            private Func<object, bool> canExecute;

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public Command(Action<object> execute, Func<object, bool> canExecute = null)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return canExecute == null || canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                execute(parameter);
            }
        }
    }
}
