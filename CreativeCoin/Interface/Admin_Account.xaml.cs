using System;
using System.Windows;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_Account.xaml
    /// </summary>
    public partial class Admin_Account : Window
    {
        public Admin_Account()
        {
            InitializeComponent();
        }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            AccountTable.ItemsSource = DBConnection.retrieveAllAccount();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (AccountTable.IsReadOnly) AccountTable.IsReadOnly = false;
            else AccountTable.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account account = (Account)AccountTable.SelectedItem;
                DBConnection.updateAccountByUsername(account);
                MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if(AccountTable.IsReadOnly)
            {
                Admin_MainMenu adminMainMenu = new Admin_MainMenu();
                adminMainMenu.Show();
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    AccountTable.IsReadOnly = true;
                    Back_Click(sender, e);
                }
                else return;
            }
        }
    }
}
