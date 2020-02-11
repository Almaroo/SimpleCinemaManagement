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
    /// Logika interakcji dla klasy Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        Admin a;
        Cinema cinema;
        public Schedule(Cinema c)
        {
            a = (Admin)App.Current.Properties["currentObject"];
            cinema = c;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void UpdateScheduleListview()
        {
            ScheduleListview.ItemsSource = cinema.Schedule;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateScheduleListview();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddSchedule addschedule = new AddSchedule(cinema);
            addschedule.ShowDialog();
            UpdateScheduleListview();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Showing selectedShowing = (Showing)ScheduleListview.SelectedItem;

            if(selectedShowing != null)
            {
                a.GetCinema(cinema.CinemaId).First().Schedule.Remove(selectedShowing);
            }
            UpdateScheduleListview();
        }
    }
}
