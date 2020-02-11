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
    /// Logika interakcji dla klasy ManageUser.xaml
    /// </summary>
    public partial class ManageUser : Window
    {
        Admin a;
        User user;
        public ManageUser(User u)
        {
            a = (Admin)App.Current.Properties["currentUser"];
            user = u;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void UpdateManagedCinemasListview()
        {
            ManagedCinemasListview.ItemsSource = user.ManagedCinemas;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            user.ManagedCinemas.Add((Cinema)CinemasListview.SelectedItem);
            UpdateManagedCinemasListview();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            user.ManagedCinemas.Remove((Cinema)ManagedCinemasListview.SelectedItem);
            UpdateManagedCinemasListview();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateManagedCinemasListview();
            CinemasListview.ItemsSource = a.GetCinema("all");
            UserIDLabel.Content = user.UserId;
        }
    }
}
