using System.Windows;
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
            if (DBConnection.verifiedAccount(Username.Text, PhoneNumber.Text))
            {
                Account foundAccount = DBConnection.retrieveAccount(Username.Text);
                Username.Text = foundAccount.username;
                ParentName.Text = foundAccount.full_name;
                Birthdate.Text = foundAccount.birthdate == null ? "None" : foundAccount.birthdate;
                PhoneNumber.Text = foundAccount.phone_number == "" ? "None" : foundAccount.phone_number;
                NewPassword.IsEnabled = true;
                ConfirmPassword.IsEnabled = true;
                return true;
            }
            else
            {
                Username.Text = "";
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
                Account foundAccount = DBConnection.retrieveAccount(Username.Text);
                if (NewPassword.Password.Length != 0 && foundAccount.password != hashedPassword)
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

        private void PhoneNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
