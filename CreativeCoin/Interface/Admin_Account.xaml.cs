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
        private bool isSave;
        private bool isEdit;

        public Admin_Account()
        {
            InitializeComponent();
            isEdit = false;
            isSave = false;
        }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            AccountTable.ItemsSource = DBConnection.retrieveAllAccount();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (AccountTable.IsReadOnly)
            {
                AccountTable.IsReadOnly = false;
                isEdit = true;
            }
            else
            {
                AccountTable.IsReadOnly = true;
                isEdit = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Account account = (Account)AccountTable.SelectedItem;
                if(account != null)
                {
                    DBConnection.updateAccountByUsername(account);
                    MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    isSave = true;
                }
                else MessageBox.Show("There is no changed data!", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                if (!isSave)
                {
                    MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes) AccountTable.IsReadOnly = true;
                    else return;
                }
            }
            Admin_MainMenu adminMainMenu = new Admin_MainMenu();
            adminMainMenu.Show();
            this.Close();
        }
    }
}
