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
    /// Logika interakcji dla klasy ManageTickets.xaml
    /// </summary>
    public partial class ManageTickets : Window
    {
        Showing showing;
        public ManageTickets(Showing s)
        {
            showing = s;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            int row = Convert.ToInt32(RowInput.Text);
            int seat = Convert.ToInt32(SeatInput.Text);
            bool isDiscounted = (bool)IsDiscountedCheckbox.IsChecked;

            showing.SellTicket(row, seat, isDiscounted);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowingIDLabel.Content = showing.ShowingId;
            PriceLabel.Content = showing.Price;
        }

        private void IsDiscountedCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            PriceLabel.Content = showing.Price/2;
        }

        private void IsDiscountedCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            PriceLabel.Content = showing.Price;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CinemasDatabase.GetInstance.Serialize();
        }
    }
}
