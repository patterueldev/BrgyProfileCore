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
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        public UpdatePasswordWindow()
        {
            InitializeComponent();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (oldPasswordBox.Password.Trim() == "")
            {
                MessageBox.Show("Old password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (newPasswordBox.Password.Trim() == "")
            {
                MessageBox.Show("New password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (confirmPasswordBox.Password.Trim() == "")
            {
                MessageBox.Show("Confirm password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var currentUsername = BrgySession.current.Username;
            var oldPassword = oldPasswordBox.Password;
            var newPassword = newPasswordBox.Password;
            var confirmPassword = confirmPasswordBox.Password;

            var db = new BrgyContext();
            var matchedUser = db.Users.Where(u => u.Username == currentUsername).ToList().First();
            if (matchedUser != null)
            {
                var validated = PasswordHasher.Validate(oldPassword, matchedUser.Password);
                if (validated)
                {
                    if(newPassword != confirmPassword)
                    {
                        MessageBox.Show("Your new password doesn't match.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }


                    Func<string, string> hashedPassword = (password) =>
                    {
                        //Use Blake2b algorithm with saltsize of 20
                        var passwordHasher = PasswordHasherInstance.Create(HashAlgorithm.Blake2b, 20);
                        string hashedPassword = passwordHasher.Hash(password); //AED9BF19B9D5DEB3A...

                        return hashedPassword;
                    };

                    matchedUser.Password = hashedPassword(newPassword);
                    db.Update(matchedUser);
                    db.SaveChanges();

                    MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Your old password is invalid.", "Invalid Password", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Couldn't find user.", "Invalid User", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
