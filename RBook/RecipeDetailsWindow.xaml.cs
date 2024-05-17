using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RBook
{
    /// <summary>
    /// Interaction logic for RecipeDetailsWindow.xaml
    /// </summary>
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();
            var client = new HttpClient();
            var response =  client.GetAsync($"https://localhost:7047/api/Recipe/{recipe.RecipeId}");
            string responseBody = await response.Content.ReadAsStringAsync();
        }
    }
}
