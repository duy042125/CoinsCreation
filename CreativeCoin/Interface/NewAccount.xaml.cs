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

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmPassword.Password != newPassword.Password)
            {
                MessageBox.Show("The confirm password does not match your password", "Password Confirm", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DBConnection.openConnection();
                DBConnection.createAccount(newUsername.Text, Hashing.HashPassword(newPassword.Password), newParentName.Text);

                MessageBox.Show("You created a new account", "Creation Confirm", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow backToLogin = new MainWindow();
                backToLogin.Show();
                this.Close();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //Need to check if save here
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Check_Confirm_Password(object sender, KeyboardFocusChangedEventArgs e)
        {
            
        }
    }
}
