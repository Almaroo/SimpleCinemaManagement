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
    /// Logika interakcji dla klasy Users.xaml
    /// </summary>
    public partial class Users : Window
    {
        Admin a;
        User selectedUser;
        public Users()
        {
            a = (Admin)App.Current.Properties["currentUser"];
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void UpdateUsersList()
        {
            usersListview.ItemsSource = a.GetUsers("all");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = (User)usersListview.SelectedItem;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == a)
            {
                MessageBox.Show("You cannot delete yourself!", "Warning");
                return;
            }

            if (selectedUser != null)
            {
                a.DeleteUsers(selectedUser.UserId);
                UpdateUsersList();
            }
            else
            {
                MessageBox.Show("You have to select a user!", "Warning");
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
            UpdateUsersList();
        }

        private void UsersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext;
            if (item != null)
            {
                User managedUser = (User)item;
                ManageUser manageUser = new ManageUser(managedUser);
                manageUser.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateUsersList();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UsersDatabase.GetInstance.Serialize();
        }
    }
}
