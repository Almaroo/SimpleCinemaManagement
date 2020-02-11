using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logika interakcji dla klasy Cinemas.xaml
    /// </summary>
    public partial class Cinemas : Window
    {
        Admin a;
        public Cinemas()
        {
            a = (Admin)App.Current.Properties["currentUser"];
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void UpdateCinemasList()
        {
            CinemasListview.ItemsSource = a.GetCinema("all");
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddCinema addCinema = new AddCinema();
            addCinema.ShowDialog();
            UpdateCinemasList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Cinema selectedCinema = (Cinema)CinemasListview.SelectedItem;
            if (selectedCinema != null)
                a.DeleteCinema(selectedCinema.CinemaId);
            UpdateCinemasList();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateCinemasList();
        }

        private void CinemasListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                Cinema managedCinema = (Cinema)item;
                managedCinema.ScreeningRoomCount = managedCinema.ScreeningRoomsTemplates.Count;
                ManageCinema manageCinema = new ManageCinema(managedCinema);
                manageCinema.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CinemasDatabase.GetInstance.Serialize();
        }
    }
}
