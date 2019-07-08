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
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        public NewAccount()
        {
            InitializeComponent();
        }

        #region Check

        private bool checkUsername()
        {
            if (DBConnection.verifiedUsedUsername(Username.Text))
            {
                Warning.Content = "This username is taken.";
                return false;
            }
            else
            {
                if (Username.Text.Length <= 3)
                {
                    Warning.Content = "This username is too short.";
                    return false;
                }
                Warning.Content = "This username is usable.";
                return true;
            }
        }

        private bool checkSSN()
        {
            string ssn = SSN.Text;
            if (ssn.Length == 10)
            {
                for (int i = 0; i < ssn.Length; i++)
                {
                    if (ssn[i] > 57 || ssn[i] < 48) return false;
                }
                return true;
            }
            return false;
        }

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

        private bool checkRequiredFields()
        {
            if (Username.Text.Equals("") || NewPassword.Password.Equals("") || ConfirmPassword.Password.Equals("") || SSN.Text.Equals("")) return false;
            return true;
        }

        #endregion

        #region Click

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!checkRequiredFields())
            {
                MessageBox.Show("Fill up the required fields", "Required fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(!checkSSN())
            {
                MessageBox.Show("Your SSN format is not right", "SSN field", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (checkUsername() && checkPassword())
                {
                    DBConnection.openConnection();
                    DBConnection.createAccount(Username.Text, Hashing.HashPassword(NewPassword.Password), newParentName.Text, DateTimeConverter.toDateTime(Birthdate.Text), PhoneNumber.Text, SSN.Text);

                    MessageBox.Show("You created a new account.", "Creation Confirm", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow backToLogin = new MainWindow();
                    backToLogin.Show();
                    this.Close();
                    return;
                }
                MessageBox.Show("Check the required fields", "Required fields", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to go back ?", "Go Back", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
        }

        private void Check_Click(object sender, RoutedEventArgs e)
        {
            if (checkUsername()) return;
        }

        private void ConfirmPassword_Check(object sender, RoutedEventArgs e)
        {
            if (checkPassword()) return;
        }

        #endregion
    }
}
