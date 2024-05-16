using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using CookBook.Models;
using System.Windows.Input;
using CookBook.Controllers;
using DP.Spoonacular;


namespace RBook.ViewModels
{
    public class SpoonAcularVM : INotifyPropertyChanged
    {
        public ObservableCollection<Recipe> ExternalRecipes { get; set; }

        public SpoonAcularVM()
        {
            ExternalRecipes = new ObservableCollection<Recipe>();

            ExternalRecipes.Add(new Recipe { Title = "Recipe 1", Description = "Description 1" });
            ExternalRecipes.Add(new Recipe { Title = "Recipe 2", Description = "Description 2" });
            ExternalRecipes.Add(new Recipe { Title = "Recipe 3", Description = "Description 3" });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
