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
    /// Logika interakcji dla klasy AddCinema.xaml
    /// </summary>
    public partial class AddCinema : Window
    {
        Admin a;
        public AddCinema()
        {
            a = (Admin)App.Current.Properties["currentUser"];
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameInput.Text;
            string city = CityInput.Text;
            string street = StreetInput.Text;
            int buildingNumber = Convert.ToInt32(NumberInput.Text);

            a.AddCinema(name, new Address(city, street, buildingNumber));
            Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
