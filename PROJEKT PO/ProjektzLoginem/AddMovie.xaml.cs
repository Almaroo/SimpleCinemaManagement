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
    /// Logika interakcji dla klasy AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Window
    {
        Admin a;
        public AddMovie()
        {
            a = (Admin)App.Current.Properties["currentUser"];
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleInput.Text;
            int duration = Convert.ToInt32(DurationInput.Text);

            a.AddMovie(title, duration);
            Close();
        }
    }
}
