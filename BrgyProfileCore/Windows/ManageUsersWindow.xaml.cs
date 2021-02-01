using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using BrgyProfileCore.Core;

namespace BrgyProfileCore.Windows
{
    /// <summary>
    /// Interaction logic for ManageUsersWindow.xaml
    /// </summary>
    public partial class ManageUsersWindow : Window
    {
        User selectedUser
        {
            get
            {
                // loads index of selected row
                var idx = usersDataGrid.SelectedIndex;

                if (idx < 0 || idx >= usersDataGrid.Items.Count)
                {
                    return null;
                }
                return (User)usersDataGrid.Items[idx];
            }
        }
        public ManageUsersWindow()
        {
            InitializeComponent();
        }

        private void usersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // loads index of selected row
            var idx = usersDataGrid.SelectedIndex;

            // toggles buttons
            EditUserButton.IsEnabled = idx >= 0;
            DeleteUserButton.IsEnabled = idx >= 0;

            if(selectedUser != null)
            {
                EditUserButton.IsEnabled = selectedUser.UserId != BrgySession.current.UserId;
                DeleteUserButton.IsEnabled = selectedUser.UserId != BrgySession.current.UserId;
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = SearchTextBox.Text;
            this.refreshList(keyword);
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList(string keyword = null)
        {
            var db = new BrgyContext();
            usersDataGrid.ItemsSource = db.Users
                .ToList()
                .FindAll(user =>
                {
                    if (keyword == null)
                    {
                        return true;
                    }
                    return user.Username.ToLower().Contains(keyword.ToLower()) ||
                        user.Name.ToLower().Contains(keyword.ToLower());
                });
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpsertUserWindow();
            window.ShowDialog();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.refreshList();
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
