using System;
using System.Windows;
using System.Windows.Media;
using Middleware;
using Middleware.Database_Component;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Admin_Report.xaml
    /// </summary>
    public partial class Admin_Report : Window
    {
        private bool isSave;
        private bool isEdit;

        public Admin_Report()
        {
            InitializeComponent();
            isSave = false;
            isEdit = false;
        }

        private void DataTable_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable.ItemsSource = DBConnection.retrieveAllReports();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (DataTable.IsReadOnly)
            {
                DataTable.IsReadOnly = false;
                isEdit = true;
                EditButton.Content = "Save";
                DeleteButton.Visibility = Visibility.Visible;
                State.Background = Brushes.Coral;
            }
            else
            {
                Report data = (Report)DataTable.SelectedItem;
                if (data != null)
                {
                    DBConnection.updateReportByKeys(data);
                    MessageBox.Show("Data Saved", "Saved Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    isSave = true;
                }
                DataTable.IsReadOnly = true;
                isEdit = false;
                EditButton.Content = "Edit";
                DeleteButton.Visibility = Visibility.Hidden;
                State.Background = Brushes.LightCoral;
            }
            DataTable.UnselectAllCells();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure to delete this Child ?", "Deletion Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Report data = (Report)DataTable.SelectedItem;
                    DBConnection.deleteReport(data);
                    DataTable_Loaded(sender, e);
                }
                else return;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                if (!isSave)
                {
                    MessageBoxResult result = MessageBox.Show("Your data is unsave. Do you want to go back ?", "Unsaved Data", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes) DataTable.IsReadOnly = true;
                    else return;
                }
            }
            Admin_MainMenu adminMainMenu = new Admin_MainMenu();
            adminMainMenu.Show();
            this.Close();
        }
    }
}
