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
    /// Logika interakcji dla klasy SelectShowing.xaml
    /// </summary>
    public partial class SelectShowing : Window
    {
        User user;
        Cinema cinema;
        public SelectShowing(Cinema c)
        {
            cinema = c;
            user = (User)App.Current.Properties["currentUser"];
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void ShowingsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                Showing managedShowing = (Showing)item;
                ManageTickets manageTickets = new ManageTickets(managedShowing);
                manageTickets.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowingsListview.ItemsSource = cinema.Schedule;
            CinemaIDLabel.Content = cinema.CinemaId;
        }
    }
}
