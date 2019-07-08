using System;
using System.Collections.Generic;
using System.Globalization;
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
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for ForgetAccount.xaml
    /// </summary>
    public partial class ForgetAccount : Window
    {
        public ForgetAccount()
        {
            InitializeComponent();
        }

        #region Check

        private Account temp = new Account();

        private bool checkPassword()
        {
            if (ConfirmPassword.Password.Equals(NewPassword.Password))
            {
                Match.Content = "Passwords match.";
                return true;
            }
            Match.Content = "Passwords don't match.";
            return false;
        }

        private bool checkUsername()
        {

            if (DBConnection.verifiedUsername(Username.Text, SSN.Text, ref temp))
            {
                Username.Text = temp.username;
                SSN.Text = temp.SSN;
                ParentName.Text = temp.full_name;

                if (temp.birthdate == null) Birthdate.Text = "";
                else Birthdate.Text = DateTimeConverter.toString(temp.birthdate);

                PhoneNumber.Text = temp.phone_number;
                NewPassword.IsEnabled = true;
                ConfirmPassword.IsEnabled = true;
                return true;
            }
            else
            {
                Username.Text = "";
                SSN.Text = "";
                ParentName.Text = "";
                Birthdate.Text = "";
                PhoneNumber.Text = "";
                NewPassword.IsEnabled = false;
                ConfirmPassword.IsEnabled = false;
                Warning.Content = "No account found !";
                return false;
            }
        }

        #endregion

        #region Click

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (checkUsername()) return;
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string hashedPassword = Hashing.HashPassword(NewPassword.Password);
            if (checkUsername())
            {
                if(NewPassword.Password.Length != 0 && temp.password != hashedPassword)
                {
                    DBConnection.changePassword(Username.Text, hashedPassword);
                    MessageBox.Show("You changed your password.", "Change Password", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow backToLogIn = new MainWindow();
                    backToLogIn.Show();
                    this.Close();
                }
                else MessageBox.Show("Please enter new password. This password is your previous password.", "Change Password", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else MessageBox.Show("Cannot find your account.", "Change Password", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void ConfirmPassword_Check(object sender, RoutedEventArgs e)
        {
            if (checkPassword()) return;
        }

        #endregion
    }
}
