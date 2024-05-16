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
    public class RecipeVM : INotifyPropertyChanged
    {
        public ObservableCollection<Recipe> Recipes { get; set; }

        public RecipeVM()
        {
            Recipes = new ObservableCollection<Recipe>();

            Recipes.Add(new Recipe { Title = "Recipe 1", Description = "Description 1" });
            Recipes.Add(new Recipe { Title = "Recipe 2", Description = "Description 2" });
            Recipes.Add(new Recipe { Title = "Recipe 3", Description = "Description 3" });

            OpenRecipeCommand = new RelayCommand(OpenRecipe);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand OpenRecipeCommand { get; set; }
       
        private void OpenRecipe(object parameter)
        {
            Recipe selectedRecipe = parameter as Recipe;
            if (selectedRecipe != null)
            {
                // כאן אתה יכול לפתוח עמוד חדש ולהעביר את המתכון הנבחר לעמוד החדש
                // לדוגמה:
                // RecipePage recipePage = new RecipePage(selectedRecipe);
                // recipePage.Show();
            }
        }

    }

    //public class RelayCommand : ICommand
    //{
    //    private Action<object> execute;

    //    public RelayCommand(Action<object> executeAction)
    //    {
    //        execute = executeAction;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        execute(parameter);
    //    }
    //    //private Recipe CurrentModel;

    //    //public event PropertyChangedEventHandler PropertyChanged;

    //    ////public AddFoodCommand Add { get; set; }

    //    //public ObservableCollection<Recipe> Recipes { get; set; }
    //    //public RecipeVM()
    //    //{
    //    //    CurrentModel = new Recipe();
    //    //    Foods = new ObservableCollection<Recipe>(CurrentModel.Foods);
    //    //    Add = new AddFoodCommand();
    //    //    Foods.CollectionChanged += Foods_CollectionChanged;
    //    //    Add.NewFoodDataArrived += Add_NewFoodDataArrived;
    //    //}
    //}

    // הוספת קומנד לפתיחת עמוד חדש עם לחיצה על מתכון
    

    public class RelayCommand : ICommand
    {
        private Action<object> execute;

        public RelayCommand(Action<object> executeAction)
        {
            execute = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
