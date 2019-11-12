using System;
using System.Windows;
using Middleware;

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

        private bool checkUsedUsername()
        {
            if (DBConnection.verifiedUsedUsername(Username.Text))
            {
                WarningUsername.Content = "This username is taken.";
                return false;
            }
            else
            {
                if (Username.Text.Length <= 3)
                {
                    WarningUsername.Content = "This username is too short.";
                    return false;
                }
                WarningUsername.Content = "This username is usable.";
                return true;
            }
        }

        private bool checkPassword()
        {
            if (ConfirmPassword.Password.Equals(NewPassword.Password))
            {
                MatchPassword.Content = "Passwords match.";
                return true;
            }
                MatchPassword.Content = "Passwords don't match.";
                return false;
        }

        private bool checkRequiredFields()
        {
            if (Username.Text.Equals("") || NewPassword.Password.Equals("") || ConfirmPassword.Password.Equals("")) return false;
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
            try
            {

                if (checkUsedUsername() && checkPassword())
                {
                    DBConnection.createAccount(Username.Text, Hashing.HashPassword(NewPassword.Password), newParentName.Text, Birthdate.SelectedDate, PhoneNumber.Text);
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
            checkUsedUsername();
        }

        private void ConfirmPassword_Check(object sender, RoutedEventArgs e)
        {
            checkPassword();
        }

        #endregion
    }
}
