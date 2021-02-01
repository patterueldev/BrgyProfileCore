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
using BrgyProfileCore.Core;
using System.Linq;
using PasswordHashing;
namespace BrgyProfileCore.Windows
{
    /// <summary>
    /// Interaction logic for UpdateUsernameWindow.xaml
    /// </summary>
    public partial class UpdateUsernameWindow : Window
    {
        public UpdateUsernameWindow()
        {
            InitializeComponent();

            currentUsernameBox.Text = BrgySession.current.Username;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (newUsernameBox.Text.Trim() == "")
            {
                MessageBox.Show("New username must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (passwordBox.Password.Trim() == "")
            {
                MessageBox.Show("Password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var currentUsername = BrgySession.current.Username;
            var newUsername = newUsernameBox.Text.Trim();
            var password = passwordBox.Password;

            var db = new BrgyContext();
            var matchedUser = db.Users.Where(u => u.Username == currentUsername).ToList().First();
            if (matchedUser != null)
            {
                var validated = PasswordHasher.Validate(password, matchedUser.Password);
                if (validated)
                {
                    matchedUser.Username = newUsername;
                    db.Update(matchedUser);
                    db.SaveChanges();
                    Properties.Settings.Default.UserLoggedIn = newUsername;
                    Properties.Settings.Default.Save();


                    MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("You have entered an invalid password", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Couldn't find user.", "Invalid User", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
