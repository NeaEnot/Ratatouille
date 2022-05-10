using Ratatouille.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ratatouille.GUI.ViewModels
{
    internal class RecipeViewModel : INotifyPropertyChanged
    {
        public Recipe Recipe { get; private set; }

        public string Id
        {
            get => Recipe.Id;
            set
            {
                Recipe.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get => Recipe.Name;
            set
            {
                Recipe.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Thumbnail
        {
            get => Recipe.Thumbnail;
            set
            {
                Recipe.Thumbnail = value;
                OnPropertyChanged("Thumbnail");
            }
        }

        public string Tags
        {
            get => Recipe.Tags;
            set
            {
                Recipe.Tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public string ApproxTime
        {
            get => Recipe.ApproxTime;
            set
            {
                Recipe.ApproxTime = value;
                OnPropertyChanged("ApproxTime");
            }
        }

        public string Ingredients
        {
            get => Recipe.Ingredients;
            set
            {
                Recipe.Ingredients = value;
                OnPropertyChanged("Ingredients");
            }
        }

        public string Tools
        {
            get => Recipe.Tools;
            set
            {
                Recipe.Tools = value;
                OnPropertyChanged("Tools");
            }
        }

        public string Instruction
        {
            get => Recipe.Instruction;
            set
            {
                Recipe.Instruction = value;
                OnPropertyChanged("Instruction");
            }
        }

        public string Notes
        {
            get => Recipe.Notes;
            set
            {
                Recipe.Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        public List<string> Links
        {
            get => Recipe.Links;
            set
            {
                Recipe.Links = value;
                OnPropertyChanged("Links");
            }
        }

        public List<string> Images
        {
            get => Recipe.Images;
            set
            {
                Recipe.Images = value;
                OnPropertyChanged("Images");
            }
        }

        internal RecipeViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
