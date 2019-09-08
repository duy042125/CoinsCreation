using System;
using System.Windows;
using System.Collections.Generic;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_Children.xaml
    /// </summary>
    public partial class Admin_Children : Window
    {
        private bool isSave;

        public Admin_Children()
        {
            InitializeComponent();
            isSave = false;
        }

        private void AccountTable_Loaded(object sender, RoutedEventArgs e)
        {
            List<Child> child = DBConnection.retrieveAllChildren();
            ChildrenTable.ItemsSource = DBConnection.retrieveAllChildren();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ChildrenTable.IsReadOnly) ChildrenTable.IsReadOnly = false;
            else ChildrenTable.IsReadOnly = true;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Child child = (Child)ChildrenTable.SelectedItem;
                DBConnection.updateChildByKeys(child);
                MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
                isSave = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (!isSave)
            {
                MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes) ChildrenTable.IsReadOnly = true;
                else return;
            }
            Admin_MainMenu adminMainMenu = new Admin_MainMenu();
            adminMainMenu.Show();
            this.Close();
        }
    }
}
