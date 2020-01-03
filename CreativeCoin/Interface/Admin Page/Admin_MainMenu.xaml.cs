using System.Windows;
using Middleware;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_MainMenu.xaml
    /// </summary>
    public partial class Admin_MainMenu : Window
    {
        public Admin_MainMenu()
        {
            InitializeComponent();
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            Admin_Account adminAccount = new Admin_Account();
            adminAccount.Show();
            this.Close();
        }

        private void Children_Click(object sender, RoutedEventArgs e)
        {
            Admin_Children accountChildren = new Admin_Children();
            accountChildren.Show();
            this.Close();
        }

        private void Report_Click(object sender, RoutedEventArgs e)
        {
            Admin_Report adminReport = new Admin_Report();
            adminReport.Show();
            this.Close();
        }

        private void Behavior_Click(object sender, RoutedEventArgs e)
        {
            Admin_Behavior adminBehavior = new Admin_Behavior();
            adminBehavior.Show();
            this.Close();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult warning = MessageBox.Show("Are you sure to log out ?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (warning == MessageBoxResult.Yes)
            {
                MainWindow backToLogIn = new MainWindow();
                backToLogIn.Show();
                this.Close();
                LogInInformation.Clear();
            }
        }
    }
}
