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
using ProjektZaliczeniowyFinale;

namespace tloo
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            User currentUser = (User)App.Current.Properties["currentUser"];
            if(currentUser != null)
            {
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("User not logged in!");
                return;
            }
        }

        private void Click_button_users(object sender, RoutedEventArgs e)
        {
            Users u = new Users();
            u.Show();
            
        }
        private void Click_button_movies(object sender, RoutedEventArgs e)
        {
            Movies m = new Movies();
            m.Show();
           
        }
        private void Click_button_cinemas(object sender, RoutedEventArgs e)
        {
            Cinemas c = new Cinemas();
            c.Show();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            User u = (User)App.Current.Properties["currentUser"];
            userIDLabel.Content = u.UserId;
        }
    }
}
