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
    /// Logika interakcji dla klasy ManageCinema.xaml
    /// </summary>
    public partial class ManageCinema : Window
    {
        Cinema cinema;
        public ManageCinema(Cinema c)
        {
            cinema = c;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule(cinema);
            schedule.Show();
        }

        private void ScreeningRoomsButton_Click(object sender, RoutedEventArgs e)
        {
            ScreeningRooms screeningRooms = new ScreeningRooms(cinema);
            screeningRooms.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CinemaIDLabel.Content = cinema.CinemaId;
        }
    }
}
