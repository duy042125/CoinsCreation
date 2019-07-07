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
    /// Interaction logic for ForgetAccount.xaml
    /// </summary>
    public partial class ForgetAccount : Window
    {
        public ForgetAccount()
        {
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Account temp = new Account();

            if (DBConnection.verifiedUsername(Username.Text, SSN.Text, ref temp))
            {
                ParentName.Text = temp.full_name;
                Birthdate.Text = temp.birthdate.ToString();
                PhoneNumber.Text = temp.phone_number;
                newPassword.IsEnabled = true;
                confirmPassword.IsEnabled = true;
            }
            else
            {
                Warning.Content = "No account found !";
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
