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
    /// Interaction logic for UpsertUserWindow.xaml
    /// </summary>
    public partial class UpsertUserWindow : Window
    {
        BrgyContext db = new BrgyContext();
        User user;
        public UpsertUserWindow()
        {
            InitializeComponent();
        }
        public UpsertUserWindow(User user)
        {
            InitializeComponent();

            this.user = db.Users.Where(u => u.UserId == user.UserId).ToList().First();
            newUsernameBox.Text = user.Username;
            nameBox.Text = user.Name;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var username = newUsernameBox.Text.Trim();
            var name = nameBox.Text.Trim();
            var password = passwordBox.Password;
            var confirm = confirmPasswordBox.Password;

            if(username == "")
            {
                MessageBox.Show("Username must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (name == "")
            {
                MessageBox.Show("Name must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var existingUsers = db.Users.Where(u => u.Username == username).ToList();
            if (existingUsers.Count > 0)
            {
                var willValidate = false;
                if (this.user == null)
                {
                    willValidate = true;
                }
                else
                {
                    willValidate = existingUsers.First().UserId != this.user.UserId;
                }

                if (willValidate)
                {
                    MessageBox.Show($"{username} is already used.", "Invalid Username", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }

            Func<string, string> hashedPassword = (password) =>
            {
                //Use Blake2b algorithm with saltsize of 20
                var passwordHasher = PasswordHasherInstance.Create(HashAlgorithm.Blake2b, 20);
                string hashedPassword = passwordHasher.Hash(password); //AED9BF19B9D5DEB3A...

                return hashedPassword;
            };

            if (this.user == null)
            {
                if (password == "")
                {
                    MessageBox.Show("Password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (confirm == "")
                {
                    MessageBox.Show("Confirm password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if(password != confirm)
                {
                    MessageBox.Show("Passwords don't match!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                // Is Adding
                var user = new User
                {
                    Username = username,
                    Password = hashedPassword(password),
                    Role = "Standard",
                    Name = name
                };
                db.Add(user);
            }
            else
            {
                // Is Editing
                this.user.Username = username;
                this.user.Name = name;

                if(password != "" || confirm != "")
                {
                    // will update password
                    if (password == "")
                    {
                        MessageBox.Show("Password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (confirm == "")
                    {
                        MessageBox.Show("Confirm password must not be empty.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (password != confirm)
                    {
                        MessageBox.Show("Passwords don't match!", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    this.user.Password = hashedPassword(password);
                }

                db.Update(this.user);
            }

            db.SaveChanges();

            MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
