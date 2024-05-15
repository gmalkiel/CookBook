using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void SearchLocal_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchBox.Text;
            // כאן נבצע חיפוש מתכונים במקומי מהמאגר הנתונים המקומי
            // לדוגמה: recipeListView.ItemsSource = RecipeRepository.SearchLocalRecipes(keyword);
        }

        private void SearchExternal_Click(object sender, RoutedEventArgs e)
        {
            string keyword = searchBox.Text;
            // כאן נבצע חיפוש מתכונים במאגר חיצוני, כמו REST API או אחר
            // לדוגמה: recipeListView.ItemsSource = ExternalAPI.SearchExternalRecipes(keyword);
        }
        //private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
