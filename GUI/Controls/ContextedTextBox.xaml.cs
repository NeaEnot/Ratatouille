using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ratatouille.GUI.Controls
{
    public partial class ContextedTextBox : UserControl
    {
        public string Text
        {
            get => tb.Text;
            set => tb.Text = value;
        }

        public Action<List<string>> UpdateLinks;

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
                MenuItem item = new MenuItem
                {
                    Header = $"{i}: {links[i]}",
                    Command = new Command(e => { tb.Text.Insert(tb.CaretIndex, $"<img_{i};400x300>"); })
                };

                miInsertFromCurrent.Items.Add(item);
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
