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
    /// Logika interakcji dla klasy AddScreeningRoom.xaml
    /// </summary>
    public partial class AddScreeningRoom : Window
    {
        Cinema cinema;
        Admin a;
        public AddScreeningRoom(Cinema c)
        {
            a = (Admin)App.Current.Properties["currentUser"];
            cinema = c;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int rows = Convert.ToInt32(RowsInput.Text);
            int seats = Convert.ToInt32(SeatsInput.Text);

            a.AddScreeningRoomToCinema(cinema.CinemaId, new int[] { rows, seats });
            Close();

        }
    }
}
