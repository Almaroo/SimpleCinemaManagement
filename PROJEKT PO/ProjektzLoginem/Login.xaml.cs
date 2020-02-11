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
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        User currentUser;
        public Login()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            currentUser = UsersDatabase.LogIn(loginInput.Text, passwordInput.Text);
            App.Current.Properties["currentUser"] = currentUser;

            if (currentUser != null)
            {
                if(currentUser is Admin a)
                {
                    a.ConnectDatabases();
                    //a.LoadUpDatabases();
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                }
                else
                {
                    UserPanel userPanel = new UserPanel();
                    userPanel.Show();
                }
                this.Close();
            }
            else
                MessageBox.Show("Wrong login or password");
        }
    }
}
