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
using PasswordHashing;
using BrgyProfileCore.Core;

namespace BrgyProfileCore.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public MainWindow mainWindow;
        bool willLogin = false;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text.Trim();
            var password = passwordBox.Password.Trim();

            if(username == "")
            {
                ShowInvalidInputMessage("Username must not be empty");
                return;
            }

            if (password == "")
            {
                ShowInvalidInputMessage("Password must not be empty");
                return;
            }

            if (BrgySession.LoginUser(username, password))
            {
                willLogin = true;
                mainWindow.Visibility = Visibility.Visible;
                Close();
            }
            else
            {
                MessageBox.Show("You have entered an invalid credential", "Invalid Credential", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        void ShowInvalidInputMessage(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!willLogin)
            {
                mainWindow.Close();
            }
        }
    }
}
