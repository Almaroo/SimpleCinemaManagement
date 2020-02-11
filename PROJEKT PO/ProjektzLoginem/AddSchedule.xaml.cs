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
    /// Logika interakcji dla klasy AddSchedule.xaml
    /// </summary>
    public partial class AddSchedule : Window
    {
        Cinema cinema;
        Admin a;
        public AddSchedule(Cinema c)
        {
            cinema = c;
            a = (Admin)App.Current.Properties["currentUser"];
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = (DateTime)DateInput.SelectedDate;
            Movie selectedMovie = (Movie)MovieInput.SelectedItem;
            ScreeningRoom selectedScreeningRoom = (ScreeningRoom)ScreeningRoomInput.SelectedItem;
            bool isPremiere = (bool)IsPremiereCheckbox.IsChecked;
            bool is3D = (bool)Is3DCheckbox.IsChecked;

            a.AddShowingToCinemaSchedule(cinema.CinemaId, selectedScreeningRoom.ScreeningRoomId, selectedMovie.MovieId, date, isPremiere, is3D);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MovieInput.ItemsSource = a.GetMovies("all");
            ScreeningRoomInput.ItemsSource = a.GetCinema(cinema.CinemaId).First().ScreeningRoomsTemplates;
        }
    }
}
