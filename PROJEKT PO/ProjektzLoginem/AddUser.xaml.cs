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
    /// Logika interakcji dla klasy AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        Admin a;
        public AddUser()
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
            string name, surname, password;
            bool isAdmin;

            name = NameInput.Text;
            surname = SurnameInput.Text;
            password = PasswordInput.Text;
            isAdmin = (bool)IsAdminCheckbox.IsChecked;

            a.AddUser(name, surname, password, isAdmin);
            Close();
        }
    }
}
