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
using System.Windows.Shapes;
using ProjektZaliczeniowyFinale;

namespace tloo
{
    /// <summary>
    /// Logika interakcji dla klasy Movies.xaml
    /// </summary>
    public partial class Movies : Window
    {
        Admin a;
        public Movies()
        {
            a = (Admin)App.Current.Properties["currentUser"];
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void UpdateMoviesListview()
        {
            MoviesListview.ItemsSource = a.GetMovies("all");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMoviesListview();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddMovie addMovie = new AddMovie();
            addMovie.ShowDialog();
            UpdateMoviesListview();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Movie selectedMovie = (Movie)MoviesListview.SelectedItem;
            if (selectedMovie != null)
                a.DeleteMovie(selectedMovie.MovieId);
            UpdateMoviesListview();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MoviesDatabase.GetInstance.Serialize();
        }
    }
}
