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
    /// Logika interakcji dla klasy ScreeningsRoom.xaml
    /// </summary>
    public partial class ScreeningRooms : Window
    {
        Cinema cinema;
        public ScreeningRooms(Cinema c)
        {
            cinema = c;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void UpdateScreeningRoomsListview()
        {
            ScreeningRoomsListview.ItemsSource = cinema.ScreeningRoomsTemplates;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddScreeningRoom addScreeningRoom = new AddScreeningRoom(cinema);
            addScreeningRoom.ShowDialog();
            UpdateScreeningRoomsListview();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ScreeningRoom selectedScreeningRoom = (ScreeningRoom)ScreeningRoomsListview.SelectedItem;
            if (selectedScreeningRoom != null)
                cinema.ScreeningRoomsTemplates.Remove(selectedScreeningRoom);
            UpdateScreeningRoomsListview();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateScreeningRoomsListview();
        }
    }
}
