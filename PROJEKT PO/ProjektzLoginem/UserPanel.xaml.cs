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
    /// Logika interakcji dla klasy UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        User user;
        public UserPanel()
        {
            user = (User)App.Current.Properties["currentUser"];
            InitializeComponent();
        }

        private void CinemasListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                Cinema managedCinema = (Cinema)item;
                SelectShowing selectShowing = new SelectShowing(managedCinema);
                selectShowing.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CinemasListview.ItemsSource = user.ManagedCinemas;
            UserIDLabel.Content = user.UserId;
        }
    }
}
